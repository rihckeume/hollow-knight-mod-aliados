# 📝 Changelog - Aliados de Inimigos

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

## [1.1.0] - 2026-07-18 (Lançamento Oficial)

### ✨ Novidades
- **Sistema de Charm "Aliados Eternos"**
  - Obtido ao derrotar o Falso Cavaleiro
  - Charm equipável no inventário do jogador
  - Gerenciamento de estado via PlayerData

- **Conversão de Inimigos em Aliados**
  - Inimigos derrotados viram aliados do jogador
  - Transformação visual: cor verde clara
  - Remoção automática de componentes de dano

- **Recrutamento em Cadeia**
  - Inimigos próximos (raio 3.5 unidades) recrutados automaticamente
  - Limite de 3 recrutamentos por evento
  - Raio configurável em `AllyManager.RECRUITMENT_RADIUS`

- **Sistema de Gerenciamento de Aliados**
  - Rastreamento de até 15 aliados simultâneos
  - Limpeza automática de aliados entre cenas
  - Memory leak prevention com Dispose patterns

### 🐛 Correções
- **Corrigido:** Construtor inválido em `EnemyAllyTag` (MonoBehaviour não usa construtor)
  - Substituído por `Awake()` + `Initialize()` pattern

- **Corrigido:** Falta de verificação de `transform` nulo
  - Adicionado `if (newAlly.transform == null)` check em `RecruitNearbyEnemies()`
  - Proteção contra null reference exceptions

- **Corrigido:** Sem thread-safety em `AllyManager`
  - Adicionado `lock (_lockObject)` em todos os acessos ao dicionário
  - Previne race conditions em múltiplas threads

- **Corrigido:** Verificação incompleta de null em sprites filhos
  - Adicionado `if (sprite != null &&` em `ApplyAllyVisuals()`

- **Corrigido:** PlayerData.instance pode ser nulo
  - Todas as funções em `AllyCharmManager` checam null
  - Retorna valores seguros em caso de erro

### 🔧 Técnico
- Adicionado error handling completo com try-catch
- Melhorado logging com emojis e níveis de severidade
- Todas as funções críticas com documentação XML
- Namespace consistente: `InimigosViramAliados`
- .csproj atualizado com HintPath variables

### 📚 Documentação
- ✅ README.md - Guia principal
- ✅ INSTALACAO.md - Passo-a-passo de instalação
- ✅ GUIA_LANCAMENTO.md - Guia oficial baseado em padrão HK
- ✅ LICENSE - MIT License incluída
- ✅ CHECKLIST_LANCAMENTO.md - Verificações pré/pós lançamento

### 🧪 Testes
- Testado em Windows 10/11
- Testado com Hollow Knight v1.5.78.11838
- Testado com Lumafly v4.x
- Verificado sem conflitos com mods populares
- Aprovado em ambiente limpo (sem outros mods)

---

## [1.0.0] - 2026-07-18 (Versão Inicial)

### ✨ Novidades
- Estrutura base do mod
- Sistema de conversão de inimigos
- Manager de aliados
- Hooks de death event e scene load

### 🟡 Problemas Conhecidos
- ❌ Construtor em MonoBehaviour causava erro
- ❌ Sem thread-safety
- ❌ Verificações incompletas de null
- ❌ Sem sistema de charm

---

## Notas de Versão

### v1.1.0 Highlights
- 🎁 Charm system oficializado
- 🛡️ Segurança melhorada (thread-safe)
- ✅ Todos os erros críticos corrigidos
- 📖 Documentação completa
- 🚀 Pronto para lançamento oficial

### Roadmap Futuro
- 🔵 v1.2.0 - Configurações customizáveis (via ModSettings)
- 🔵 v1.3.0 - Suporte a idiomas (i18n)
- 🔵 v1.4.0 - Novos tipos de aliados com habilidades especiais
- 🔵 v2.0.0 - Sistema de respawn de aliados

---

## Como Atualizar

### De v1.0.0 para v1.1.0
1. Desativar mod antigo no Lumafly
2. Baixar nova DLL do release v1.1.0
3. Copiar para pasta de mods (substituir antigo)
4. Reativar no Lumafly
5. Reiniciar jogo

⚠️ **Importante:** Salvar jogo antes de atualizar

---

## Contribuições
Para contribuir com melhorias, abra uma issue ou pull request no GitHub:
[github.com/rihckeume/hollow-knight-mod-aliados](https://github.com/rihckeume/hollow-knight-mod-aliados)

---

**Última Atualização:** 2026-07-18  
**Versão Atual:** 1.1.0  
**Status:** ✅ Lançado
