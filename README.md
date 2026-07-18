# Mod "Aliados de Inimigos" - Hollow Knight

## 📖 Descrição

Este mod transforma inimigos derrotados em aliados do jogador no jogo **Hollow Knight (v1.5.78.11838+)**.

### Funcionalidades Principais

✅ **Conversão de Inimigos**: Ao derrotar um inimigo, ele se torna um aliado visual e funcional.

✅ **Recrutamento em Cadeia**: Inimigos próximos (raio ~3.5 unidades) também são recrutados automaticamente.

✅ **Transformação Visual**: Aliados ficam verdes para diferenciação clara.

✅ **Remoção de Dano**: Aliados não atacam mais o jogador nem outros aliados.

✅ **Limite de Performance**: Máximo de 15 aliados simultâneos para evitar lag.

✅ **Limpeza de Cena**: Aliados são resetados ao trocar de cena.

## 🛠️ Instalação

### Pré-requisitos

- **Hollow Knight** versão 1.5.78.11838 ou superior (patch de modding)
- **Lumafly** (mod manager do HK) instalado no PC
- Jogo **não-pirata** (ModInstaller requer versão legítima)

### Passos

1. **Compilar o mod** (veja seção abaixo)
2. **Copiar DLL**: Colocar `InimigosViramAliados.dll` em:
   ```
   Hollow Knight/hollow_knight_Data/Managed/Mods/
   ```
3. **Abrir o Lumafly** e ativar o mod na lista
4. **Reiniciar o jogo**

## 🔨 Compilação

### No Windows/PC

```bash
# Clonar ou extrair este repositório
git clone https://github.com/rihckeume/hollow-knight-mod-aliados.git
cd hollow-knight-mod-aliados

# Ajustar paths no .csproj para suas bibliotecas do HK
# Editar InimigosViramAliados.csproj e atualizar <HintPath>

# Compilar
dotnet build -c Release

# Saída
# bin/Release/net472/InimigosViramAliados.dll
```

### No Termux (Android)

```bash
# Instalar Ubuntu via proot
pkg install proot-distro
proot-distro install ubuntu
proot-distro login ubuntu

# Atualizar e instalar .NET SDK
apt update && apt upgrade -y
apt install dotnet-sdk-9.0 build-essential

# Clonar e compilar
git clone https://github.com/rihckeume/hollow-knight-mod-aliados.git
cd hollow-knight-mod-aliados

# Compilar
dotnet build -c Release
```

## 📊 Arquitetura do Código

### Classes Principais

| Classe | Responsabilidade |
|--------|------------------|
| `InimigosViramAliadosMod` | Classe principal; gerencia hooks |
| `EnemyConversionHandler` | Lógica de conversão de inimigo para aliado |
| `AllyManager` | Gerenciador central de aliados; tracking e limites |
| `EnemyAllyTag` | Componente de marcação (tag) para identificar aliados |

### Fluxo de Execução

```
Jogador derrota Inimigo
    ↓
OnRecieveDeathEventHook dispara
    ↓
EnemyConversionHandler.ConvertEnemyToAlly() é chamado
    ↓
┌─── Adiciona EnemyAllyTag
├─── Remove componentes de dano (DamageHero, ContactDamageHero)
├─── Aplica cor verde ao sprite
├─── Desativa IA agressiva
└─── Recruta inimigos vizinhos (cadeia)
    ↓
Aliado ativado com sucesso
```

## ⚙️ Configurações

Alterar constantes em `AllyManager.cs`:

```csharp
public const int MAX_ALLIES = 15;              // Limite de aliados
public const float RECRUITMENT_RADIUS = 3.5f; // Raio de recrutamento em unidades
```

Alterar cor dos aliados em `EnemyConversionHandler.cs`:

```csharp
private static Color ALLY_COLOR = new Color(0.5f, 1f, 0.5f, 1f); // RGB: Verde
```

## 🐛 Troubleshooting

### Mod não carrega

- ✅ Verificar se `Modding.dll` existe em `hollow_knight_Data/Managed/`
- ✅ Verificar se o jogo é a versão 1.5.78+ (downpatch de modding)
- ✅ Verificar logs no Lumafly

### Inimigos não viram aliados

- ✅ Verificar console do jogo (Ctrl+F8 em testes)
- ✅ Testar com inimigos comuns (ex: Crawling Husk)
- ✅ Revisar se `BeforeSceneLoadHook` está limpando aliados prematuramente

### Performance lenta com muitos aliados

- ✅ Diminuir `MAX_ALLIES` em `AllyManager.cs`
- ✅ Desativar `RecruitNearbyEnemies()` em `EnemyConversionHandler.cs` se necessário

## 📚 Referências

- [Hollow Knight Modding API](https://github.com/hk-modding/api)
- [Lumafly (Scarab+)](https://github.com/hk-modding/lumafly)
- [MonoMod](https://github.com/MonoMod/MonoMod)
- [Documentação .NET Framework 4.7.2](https://docs.microsoft.com/en-us/dotnet/framework/)

## 📝 Licença

Este mod é fornecido "como está" para fins educacionais e recreativos.

## ✉️ Suporte

Para problemas, dúvidas ou sugestões:

- Abrir uma **Issue** neste repositório
- Consultar o Discord de Modding do Hollow Knight
- Verificar logs do Lumafly para mais detalhes

---

**Versão**: 1.0.0  
**Compatibilidade**: Hollow Knight 1.5.78.11838+  
**Status**: Beta (testado em ambiente de desenvolvimento)