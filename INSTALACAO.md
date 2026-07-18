# Guia de Instalação Completo

## Passo 1: Verificar Pré-requisitos

### Verificar versão do Hollow Knight

1. Abra Steam
2. Clique com botão direito em **Hollow Knight**
3. Propriedades → Abas → Betas
4. Deve estar em **None** (versão final 1.5.78.11838 é a versão de modding)

### Verificar se jogo é legítimo

- Se o jogo foi comprado na Steam, está OK
- ModInstaller não funciona em versões piratas

## Passo 2: Instalar Lumafly (Mod Manager)

1. Acesse [GitHub do Lumafly](https://github.com/hk-modding/lumafly/releases)
2. Baixe a versão mais recente (ex: `Lumafly-4.x.x.exe`)
3. Execute o instalador
4. Siga as instruções na tela
5. Deixe instalar o **Modding API** automaticamente

## Passo 3: Compilar o Mod

### Opção A: Compilar no Windows

1. Instale [.NET SDK 6.0+](https://dotnet.microsoft.com/en-us/download)
   ```bash
   dotnet --version  # Verificar instalação
   ```

2. Clone este repositório:
   ```bash
   git clone https://github.com/rihckeume/hollow-knight-mod-aliados.git
   cd hollow-knight-mod-aliados
   ```

3. **IMPORTANTE**: Editar `InimigosViramAliados.csproj`
   - Abrir arquivo com editor de texto
   - Localizar linhas com `<HintPath>`
   - Ajustar caminhos para sua instalação do Hollow Knight

   Exemplo:
   ```xml
   <HintPath>C:\Program Files\Steam\steamapps\common\Hollow Knight\hollow_knight_Data\Managed\Assembly-CSharp.mm.dll</HintPath>
   ```

4. Compilar:
   ```bash
   dotnet build -c Release
   ```

5. Localizar DLL compilada:
   ```
   bin/Release/net472/InimigosViramAliados.dll
   ```

### Opção B: Compilar no Termux (Android)

```bash
# 1. Instalar Termux do F-Droid
# Link: https://f-droid.org/en/packages/com.termux/

# 2. Dentro do Termux:
pkg update && pkg upgrade -y
pkg install proot-distro git

# 3. Instalar Ubuntu
proot-distro install ubuntu
proot-distro login ubuntu

# 4. Atualizar Ubuntu e instalar .NET
apt update && apt upgrade -y
apt install -y dotnet-sdk-9.0 build-essential git

# 5. Clonar repositório
git clone https://github.com/rihckeume/hollow-knight-mod-aliados.git
cd hollow-knight-mod-aliados

# 6. Compilar (pode levar alguns minutos)
dotnet build -c Release

# 7. Localizar DLL
find . -name "InimigosViramAliados.dll" -type f
# Resultado típico:
# ./bin/Release/net472/InimigosViramAliados.dll
```

## Passo 4: Instalar o Mod

### Via Lumafly (Recomendado)

1. Abra o **Lumafly**
2. Clique em **Install Mod**
3. Selecione `InimigosViramAliados.dll` compilada
4. Mod aparecerá na lista com ✅ habilitado
5. Reinicie o Hollow Knight

### Manual (Alternativa)

1. Localize pasta de mods:
   ```
   C:\Program Files\Steam\steamapps\common\Hollow Knight\hollow_knight_Data\Managed\Mods\
   ```

2. Copie `InimigosViramAliados.dll` para essa pasta

3. Inicie o jogo

4. Verifique no console do jogo (Ctrl+F8) se carregou:
   ```
   [Aliados] Inicializando mod...
   [Aliados] Mod inicializado com sucesso!
   ```

## Passo 5: Testar o Mod

1. Inicie uma partida de Hollow Knight
2. Entre em uma sala com inimigos (ex: Kingdom's Edge)
3. Derrote um inimigo normalmente
4. O inimigo deve:
   - Ficar com cor **verde claro**
   - Não atacar o jogador
   - Atacar outros inimigos da sala

## 🎮 Como Usar

### Recursos do Mod

- **Limite**: Máximo 15 aliados ativos
- **Recrutamento**: Inimigos próximos (3.5 unidades) viram aliados em cadeia
- **Mudança de Cena**: Aliados são resetados (limpar memória)
- **Performance**: Otimizado para não causar lag significativo

### Exemplos de Uso

1. **Farming de Aliados**:
   - Entre em sala com muitos inimigos
   - Derrote 1 inimigo
   - Deixe que os aliados recrutem vizinhos
   - Pronto: equipe de 5-8 aliados!

2. **Combate Facilitado**:
   - Use aliados para distrair inimigos fortes
   - Ataque enquanto inimigos estão ocupados

3. **Exploração**:
   - Aliados podem ajudar em áreas perigosas
   - Reduz dano recebido pelo jogador

## ❌ Desinstalar

1. Abra **Lumafly**
2. Desabilite ou remova **Aliados de Inimigos** da lista
3. Ou remova arquivo `InimigosViramAliados.dll` da pasta `Mods/`
4. Reinicie o jogo

## 🆘 Problemas Comuns

### "DLL não encontrado"

**Solução**:
- Verificar se caminhos em `.csproj` estão corretos
- Copiar DLLs do Modding API para diretório do projeto se necessário

### "Mod não ativa no Lumafly"

**Solução**:
- Verificar versão do .NET (deve ser 4.7.2+)
- Recompile com `dotnet build -c Release`
- Reinicie o Lumafly

### "Game não carrega com mod ativado"

**Solução**:
- Desativar mod no Lumafly
- Verificar logs em `Hollow Knight/hollow_knight_Data/StreamingAssets/ModdingHook.log`
- Tentar com .csproj paths ajustados

### "Inimigos não viram aliados"

**Solução**:
- Testar com inimigos comuns (Crawling Husk, Husk Burrower)
- Certos inimigos especiais (chefes) podem ter IA customizada
- Verificar console do jogo (Ctrl+F8) para mensagens de erro

---

**Pronto!** O mod deve estar funcionando. Aproveite seus aliados! 🎉