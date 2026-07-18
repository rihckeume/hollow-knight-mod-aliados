#!/usr/bin/env python3
"""
Script de Compilação Automática - Mod "Aliados de Inimigos" Hollow Knight
Compatível com Windows 10 Pro

Este script:
1. Verifica se o .NET SDK está instalado
2. Baixa e instala o .NET SDK (se necessário)
3. Configura os paths das bibliotecas do Hollow Knight
4. Compila o mod
5. Gera a DLL final em bin/Release/
"""

import os
import sys
import subprocess
import platform
import urllib.request
import shutil
from pathlib import Path
import json
import tempfile
import zipfile

# Configurações
DOTNET_VERSION = "8.0"  # Versão do .NET SDK
DOTNET_DOWNLOAD_URL = "https://dot.net/v1/dotnet-install.ps1"
HOLLOW_KNIGHT_COMMON_PATHS = [
    r"C:\Program Files\Steam\steamapps\common\Hollow Knight\hollow_knight_Data\Managed",
    r"C:\Program Files (x86)\Steam\steamapps\common\Hollow Knight\hollow_knight_Data\Managed",
    os.path.expandvars(r"%APPDATA%\..\..\Program Files\Steam\steamapps\common\Hollow Knight\hollow_knight_Data\Managed"),
]

class Colors:
    """Cores para output no console"""
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKCYAN = '\033[96m'
    OKGREEN = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'
    BOLD = '\033[1m'
    UNDERLINE = '\033[4m'

def print_header(text):
    print(f"\n{Colors.HEADER}{Colors.BOLD}{'='*60}")
    print(f"  {text}")
    print(f"{'='*60}{Colors.ENDC}\n")

def print_success(text):
    print(f"{Colors.OKGREEN}✓ {text}{Colors.ENDC}")

def print_error(text):
    print(f"{Colors.FAIL}✗ {text}{Colors.ENDC}")

def print_warning(text):
    print(f"{Colors.WARNING}⚠ {text}{Colors.ENDC}")

def print_info(text):
    print(f"{Colors.OKCYAN}ℹ {text}{Colors.ENDC}")

def run_command(cmd, description=""):
    """Executa um comando e retorna sucesso/falha"""
    if description:
        print_info(f"Executando: {description}")
    try:
        result = subprocess.run(cmd, shell=True, capture_output=True, text=True, check=False)
        if result.returncode == 0:
            return True, result.stdout
        else:
            return False, result.stderr
    except Exception as e:
        return False, str(e)

def check_dotnet():
    """Verifica se o .NET SDK está instalado"""
    print_header("1. Verificando .NET SDK")
    
    success, output = run_command("dotnet --version", "Verificando versão do .NET")
    
    if success:
        print_success(f"✓ .NET SDK encontrado: {output.strip()}")
        return True
    else:
        print_warning("✗ .NET SDK não encontrado")
        return False

def install_dotnet():
    """Instala o .NET SDK no Windows 10 Pro"""
    print_header("2. Instalando .NET SDK")
    
    print_info("Baixando instalador do .NET SDK...")
    
    try:
        # Usar o instalador do .NET (melhor que PowerShell para automação)
        dotnet_installer_url = "https://dot.net/v1/dotnet-install.exe"
        installer_path = os.path.join(tempfile.gettempdir(), "dotnet-install.exe")
        
        print_info(f"Download: {dotnet_installer_url}")
        urllib.request.urlretrieve(dotnet_installer_url, installer_path)
        print_success("✓ Instalador baixado")
        
        print_info("Executando instalador (pode levar alguns minutos)...")
        success, output = run_command(
            f'"{installer_path}" -Version latest -InstallDir "C:\\Program Files\\dotnet"',
            "Instalação do .NET SDK"
        )
        
        if success:
            print_success("✓ .NET SDK instalado com sucesso")
            # Adicionar ao PATH
            os.environ['PATH'] = r"C:\Program Files\dotnet" + os.pathsep + os.environ['PATH']
            return True
        else:
            print_error(f"Erro na instalação: {output}")
            return False
            
    except Exception as e:
        print_error(f"Erro ao baixar/instalar .NET SDK: {str(e)}")
        return False

def find_hollow_knight():
    """Procura o diretório do Hollow Knight"""
    print_header("3. Localizando Hollow Knight")
    
    for path in HOLLOW_KNIGHT_COMMON_PATHS:
        if os.path.exists(path):
            print_success(f"✓ Hollow Knight encontrado em: {path}")
            return path
    
    print_warning("✗ Hollow Knight não encontrado em locais comuns")
    print_info("Locais verificados:")
    for path in HOLLOW_KNIGHT_COMMON_PATHS:
        print(f"  - {path}")
    
    print("\nPor favor, indique o caminho manualmente:")
    custom_path = input("Caminho para hollow_knight_Data/Managed: ").strip()
    
    if os.path.exists(custom_path):
        print_success(f"✓ Caminho confirmado: {custom_path}")
        return custom_path
    else:
        print_error(f"✗ Caminho inválido: {custom_path}")
        return None

def verify_hollow_knight_files(hk_path):
    """Verifica se os arquivos necessários do Hollow Knight existem"""
    print_header("4. Verificando arquivos do Hollow Knight")
    
    required_files = [
        "Assembly-CSharp.dll",
        "UnityEngine.dll",
        "Modding.dll",
    ]
    
    missing_files = []
    for file in required_files:
        file_path = os.path.join(hk_path, file)
        if os.path.exists(file_path):
            print_success(f"✓ {file}")
        else:
            print_warning(f"✗ {file} não encontrado")
            missing_files.append(file)
    
    if missing_files:
        print_warning(f"\n⚠ Alguns arquivos não foram encontrados: {', '.join(missing_files)}")
        print_info("Continuando mesmo assim... (a compilação pode falhar)")
        return False
    
    return True

def update_csproj(hk_path):
    """Atualiza o arquivo .csproj com os paths corretos"""
    print_header("5. Configurando .csproj")
    
    csproj_path = "InimigosViramAliados.csproj"
    
    if not os.path.exists(csproj_path):
        print_error(f"✗ Arquivo {csproj_path} não encontrado")
        return False
    
    try:
        with open(csproj_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        # Substituir paths (escapar backslashes)
        hk_path_escaped = hk_path.replace("\\", "\\\\")
        
        original_content = content
        
        # Atualizar referências comuns
        replacements = {
            r"\..\..\..\hollow_knight_Data\Managed": hk_path_escaped,
            r"..\..\hollow_knight_Data\Managed": hk_path_escaped,
        }
        
        for old, new in replacements.items():
            content = content.replace(old, new)
        
        # Se o conteúdo mudou, salvar
        if content != original_content:
            with open(csproj_path, 'w', encoding='utf-8') as f:
                f.write(content)
            print_success(f"✓ {csproj_path} atualizado com path correto")
        else:
            print_info(f"ℹ {csproj_path} já contém configurações apropriadas")
        
        return True
        
    except Exception as e:
        print_error(f"✗ Erro ao atualizar {csproj_path}: {str(e)}")
        return False

def compile_mod():
    """Compila o mod"""
    print_header("6. Compilando Mod")
    
    print_info("Restaurando dependências...")
    success, output = run_command("dotnet restore", "Restauração de dependências")
    
    if not success:
        print_warning(f"⚠ Aviso durante restore: {output[:200]}")
    
    print_info("Compilando em Release...")
    success, output = run_command(
        "dotnet build -c Release",
        "Compilação do mod"
    )
    
    if success:
        print_success("✓ Mod compilado com sucesso!")
        return True
    else:
        print_error(f"✗ Erro na compilação:\n{output}")
        return False

def verify_output():
    """Verifica se a DLL foi gerada"""
    print_header("7. Verificando Saída")
    
    dll_path = Path("bin/Release/net472/InimigosViramAliados.dll")
    
    if dll_path.exists():
        size_mb = dll_path.stat().st_size / (1024 * 1024)
        print_success(f"✓ DLL gerada com sucesso!")
        print(f"  Caminho: {dll_path.absolute()}")
        print(f"  Tamanho: {size_mb:.2f} MB")
        return True
    else:
        print_error(f"✗ DLL não encontrada em {dll_path}")
        return False

def show_installation_instructions():
    """Mostra instruções de instalação"""
    print_header("8. Próximos Passos")
    
    print(f"""
{Colors.OKGREEN}✓ Compilação Concluída!{Colors.ENDC}

Para instalar o mod:

1. Localize o diretório do Hollow Knight:
   {Colors.BOLD}Hollow Knight/hollow_knight_Data/Managed/Mods/{Colors.ENDC}

2. Copie o arquivo DLL compilado para lá:
   {Colors.BOLD}bin/Release/net472/InimigosViramAliados.dll{Colors.ENDC}

3. Abra o {Colors.BOLD}Lumafly{Colors.ENDC} (mod manager)

4. Ative o mod "Aliados de Inimigos" na lista

5. Reinicie o Hollow Knight

{Colors.WARNING}Dicas:{Colors.ENDC}
- Certifique-se de que o Hollow Knight é a versão 1.5.78.11838+
- O jogo deve ser a versão legítima (não pirata)
- Use o Lumafly para ativar/desativar o mod
""")

def main():
    """Função principal"""
    print(f"""
{Colors.HEADER}{Colors.BOLD}
╔══════════════════════════════════════════════════════════════╗
║    Script de Compilação - Mod Hollow Knight                  ║
║    "Aliados de Inimigos" v1.0.0                              ║
║    Windows 10 Pro                                            ║
╚══════════════════════════════════════════════════════════════╝
{Colors.ENDC}
""")
    
    # Verificar Windows
    if platform.system() != "Windows":
        print_error("Este script foi desenvolvido para Windows 10 Pro")
        sys.exit(1)
    
    # Step 1: Verificar/Instalar .NET
    if not check_dotnet():
        if not install_dotnet():
            print_error("Falha ao instalar .NET SDK")
            sys.exit(1)
    
    # Step 2: Encontrar Hollow Knight
    hk_path = find_hollow_knight()
    if not hk_path:
        print_error("Hollow Knight não foi encontrado")
        sys.exit(1)
    
    # Step 3: Verificar arquivos do HK
    verify_hollow_knight_files(hk_path)
    
    # Step 4: Atualizar .csproj
    if not update_csproj(hk_path):
        print_warning("⚠ Erro ao atualizar .csproj, tentando compilar mesmo assim...")
    
    # Step 5: Compilar
    if not compile_mod():
        print_error("Falha na compilação")
        sys.exit(1)
    
    # Step 6: Verificar saída
    if not verify_output():
        print_error("DLL não foi gerada corretamente")
        sys.exit(1)
    
    # Step 7: Mostrar instruções
    show_installation_instructions()
    
    print_success("\n✓ Processo concluído com sucesso!\n")

if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print_error("\n\nProcesso interrompido pelo usuário")
        sys.exit(130)
    except Exception as e:
        print_error(f"\n\nErro inesperado: {str(e)}")
        sys.exit(1)
