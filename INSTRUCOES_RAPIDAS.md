# ⚡ Instruções Rápidas - Aliados de Inimigos

## 🎯 Resumo Ultra-Rápido (30 segundos)

1. **Instale Lumafly** → [themulhima.github.io/Lumafly](https://themulhima.github.io/Lumafly/)
2. **Abra Lumafly** → Ele baixa o Modding API automaticamente
3. **Clique "Install Mod"** → Selecione `InimigosViramAliados.dll`
4. **Ative o mod** → Checkbox verde
5. **Inicie o jogo** via Lumafly
6. **Derrote Falso Cavaleiro** → Charm desbloqueado
7. **Equipe o charm** → Inimigos viram aliados!

---

## 💻 Compilação Rápida (Windows)

```bash
# 1. Instalar .NET SDK
winget install Microsoft.DotNet.SDK.6

# 2. Clonar repo
git clone https://github.com/rihckeume/hollow-knight-mod-aliados.git
cd hollow-knight-mod-aliados

# 3. Editar .csproj (abrir com Notepad, atualizar <HKPath>)
# Exemplo: C:\Program Files\Steam\steamapps\common\Hollow Knight

# 4. Compilar
dotnet build -c Release

# 5. DLL criada em:
# bin\Release\net472\InimigosViramAliados.dll
```

---

## 📋 Requisitos Mínimos

| Componente | Versão | Como Instalar |
|-----------|--------|---------------|
| Hollow Knight | 1.5.78.11838+ | Steam: Beta 1.5.78 |
| Lumafly | Recente | [Link Oficial](https://themulhima.github.io/Lumafly/) |
| .NET SDK | 6.0+ | `winget install Microsoft.DotNet.SDK.6` |
| Git (opcional) | Qualquer | `winget install Git.Git` |

---

## 🚨 Troubleshooting Rápido

| Problema | Solução |
|----------|--------|
| "Mod não carrega" | Verificar `ModLog.txt` em `%LocalAppData%\Team Cherry\Hollow Knight\` |
| "DLL não compila" | Verificar <HKPath> no .csproj com seu caminho real |
| "Charm não aparece" | Derrotar Falso Cavaleiro novamente |
| "Inimigos não viram aliados" | Verificar se charm está equipado no inventário |
| "Console F10 não aparece" | Editar `ModdingApi.GlobalSettings.json` → `ShowDebugLogInGame: true` |

---

## 📚 Documentação Completa

- 🔷 **Instalação Detalhada:** [INSTALACAO.md](./INSTALACAO.md)
- 🔷 **Guia Oficial:** [GUIA_LANCAMENTO.md](./GUIA_LANCAMENTO.md)
- 🔷 **Checklist:** [CHECKLIST_LANCAMENTO.md](./CHECKLIST_LANCAMENTO.md)
- 🔷 **Mudanças:** [CHANGELOG.md](./CHANGELOG.md)

---

## 🎮 Gameplay Rápido

### Obter Charm
1. Ir para **Falso Cavaleiro** (Kingdom's Edge)
2. Derrotar o boss
3. Charm "Aliados Eternos" = Desbloqueado

### Usar Charm
1. Abrir Inventário
2. Equipar "Aliados Eternos"
3. Matar inimigos normalmente
4. Inimigos viram aliados (verde) automaticamente

### Estratégia
- **Max 15 aliados** por vez
- **Até 3 vizinhos** recrutados por morte
- **Raio de recrutamento:** ~3.5 unidades
- **Limpeza automática:** Entre cenas

---

## 🔗 Links Úteis

- 🎮 **Hollow Knight:** [Steam](https://store.steampowered.com/app/367520/Hollow_Knight/)
- 🛠️ **Lumafly:** [themulhima.github.io/Lumafly](https://themulhima.github.io/Lumafly/)
- 📖 **HK Modding API:** [github.com/hk-modding/api](https://github.com/hk-modding/api)
- 💬 **Discord HK Modding:** [Comunidade Oficial](https://discord.gg/hollow-knight)
- 📦 **Repositório:** [github.com/rihckeume/hollow-knight-mod-aliados](https://github.com/rihckeume/hollow-knight-mod-aliados)

---

## ✅ Verificação Rápida (Mod Funcionando?)

```
✅ Mods carregam? → Pressione F10 no jogo
✅ Consola mostra logs? → Procure por "[Aliados]"
✅ Charm desbloqueado? → Verificar inventário após derrotar Falso Cavaleiro
✅ Inimigos viram verdes? → Matar inimigo com charm equipado
✅ Sem crashes? → ModLog.txt não tem exceções
```

**Se tudo OK:** 🎉 Mod funcionando perfeitamente!

---

**Versão:** 1.1.0  
**Status:** ✅ Lançado  
**Última Update:** 2026-07-18
