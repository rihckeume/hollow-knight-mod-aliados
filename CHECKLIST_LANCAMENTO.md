# ✅ Checklist de Lançamento - Mod "Aliados de Inimigos"

## 📋 Pré-Lançamento

### Compilação e Build
- [ ] **Versão corrigida:** Atualizar versão em `InimigosViramAliadosMod.cs` → `GetVersion()` retorna "1.1.0"
- [ ] **Build Release:** Executar `dotnet build -c Release` sem erros
- [ ] **DLL gerada:** Confirmar que `bin/Release/net472/InimigosViramAliados.dll` existe
- [ ] **Tamanho da DLL:** Verificar se o tamanho é razoável (~100-500 KB)
- [ ] **Sem dependências externas:** DLL não requer outras DLLs além das padrão do HK

### Estrutura de Arquivos
- [ ] **Repositório limpo:** Não há arquivos `.tmp`, `.bak`, ou de build temporário
- [ ] **Pasta `bin/` limpa:** Remover versões antigas, manter apenas Release final
- [ ] **Sem código-fonte desnecessário:** Apenas `.cs` e `.csproj` mantidos
- [ ] **README.md:** Presente e atualizado
- [ ] **INSTALACAO.md:** Presente e testado
- [ ] **LICENSE:** Arquivo MIT presente
- [ ] **GUIA_LANCAMENTO.md:** Documentação completa incluída

### Código-Fonte
- [ ] **Sem comentários em português quebrados:** Verificar encoding UTF-8
- [ ] **Sem `TODO` ou `FIXME`:** Remover placeholders de desenvolvimento
- [ ] **Logging completo:** Todos os pontos críticos com `Logger.Log()`
- [ ] **Try-catch em lugares-chave:** Sem crashes sem avisos
- [ ] **Sem hard-coded paths:** Caminhos dinâmicos ou relativos
- [ ] **Namespace correto:** `namespace InimigosViramAliados` em todos os arquivos

### Testes Funcionais
- [ ] **Mod carrega sem erros:** Console mostra "✅ Mod inicializado com sucesso!"
- [ ] **Boss handler ativo:** Logs indicam "Boss encounter handler inicializado"
- [ ] **Hooks subscritos:** Todos os 3 hooks aparecem nos logs
- [ ] **Derrota Falso Cavaleiro:** Charm desbloqueado (testar com `PlayerData.SetBool()`)
- [ ] **Charm equipável:** Mod reconhece charm equipado
- [ ] **Inimigos viram aliados:** Ao matar com charm ativo, cor muda para verde
- [ ] **Limite de 15 aliados:** Após 15 conversões, não aceita mais
- [ ] **Recrutamento em cadeia:** Até 3 vizinhos recrutados por conversão
- [ ] **Troca de cena:** Aliados limpos corretamente (sem crash)
- [ ] **Sem memory leak:** Monitorar memória antes/depois de várias conversões
- [ ] **Console F10:** Logs aparecem ao pressionar F10
- [ ] **ModLog.txt:** Arquivo gerado com entradas do mod

### Compatibilidade
- [ ] **Versão HK testada:** v1.5.78.11838 confirmado
- [ ] **Sem conflitos com mods populares:** Testar com "Godseeker", "Difficult", etc.
- [ ] **PlayerData não corrompido:** Salvar/carregar jogo funciona
- [ ] **Sem alterações permanentes no save:** Charm pode ser removido sem problemas
- [ ] **Plataformas testadas:** Windows (mínimo); idealmente Linux/Termux também

---

## 🚀 Preparação para Lançamento

### GitHub Setup
- [ ] **Repositório público:** `rihckeume/hollow-knight-mod-aliados` está acessível
- [ ] **Branch `main` limpo:** Nenhuma mudança pendente
- [ ] **Commits mensagens claras:** Histórico de commits bem descrito
- [ ] **Tags versionadas:** Criar tag `v1.1.0` (ex: `git tag -a v1.1.0 -m "Release v1.1.0"`)
- [ ] **Enviado para remote:** `git push origin main && git push origin v1.1.0`

### Documentação Finalizada
- [ ] **README.md:** 
  - [ ] Descrição clara do mod
  - [ ] Funcionalidades listadas
  - [ ] Instalação passo-a-passo
  - [ ] Requisitos (versão HK, Lumafly)
  - [ ] Uso (como obter charm, como usar)
  - [ ] Troubleshooting
  - [ ] Licença mencionada
  - [ ] Link para GitHub

- [ ] **INSTALACAO.md:**
  - [ ] Caminho para Lumafly
  - [ ] Passos de compilação (Windows/Linux/Termux)
  - [ ] Instruções de instalação manual
  - [ ] Verificação de funcionamento
  - [ ] Resolução de problemas comuns

- [ ] **GUIA_LANCAMENTO.md:**
  - [ ] Resumo executivo
  - [ ] Requisitos de software
  - [ ] Compilação passo-a-passo
  - [ ] Configuração do .csproj
  - [ ] Lumafly setup
  - [ ] Testes e depuração
  - [ ] Referências oficiais

- [ ] **LICENSE:**
  - [ ] Arquivo MIT ou compatível
  - [ ] Créditos inclusos (se usar código de outros)

- [ ] **CHANGELOG.md (novo):**
  - [ ] v1.1.0 - Data de lançamento
  - [ ] Listar todas as mudanças, novidades, correções
  - [ ] Versões anteriores (se houver)

### Release no GitHub
- [ ] **Criar Release:**
  - [ ] Ir para "Releases" → "Create new release"
  - [ ] Tag: `v1.1.0`
  - [ ] Título: "Aliados de Inimigos v1.1.0 - Release Oficial"
  - [ ] Descrição: Usar template abaixo

**Template de Descrição da Release:**

```markdown
# 🎮 Aliados de Inimigos v1.1.0

## 📝 Resumo
Mod para Hollow Knight que transforma inimigos derrotados em aliados. 
Obtém poder através do charm "Aliados Eternos" ao derrotar o Falso Cavaleiro.

## ✨ Novidades
- Sistema de Charm "Aliados Eternos"
- Inimigos viram aliados (cor verde)
- Recrutamento em cadeia (vizinhos próximos)
- Limite de 15 aliados para performance
- Thread-safety e verificações de null

## 🐛 Correções
- Remover construtor inválido de MonoBehaviour
- Adicionar verificações de null em transform
- Thread-safety em AllyManager
- Melhorar error handling

## 📦 Instalação

### Via Lumafly (Recomendado)
1. Abrir Lumafly
2. Clique em "Install Mod"
3. Selecione `InimigosViramAliados.dll`
4. Ative o mod
5. Reinicie o jogo

### Manual
1. Copie `InimigosViramAliados.dll` para:
   ```
   Hollow Knight/hollow_knight_Data/Managed/Mods/
   ```
2. Inicie o jogo com Lumafly

## ✅ Requisitos
- **Hollow Knight:** v1.5.78.11838+
- **Lumafly:** Versão recente (auto-instala Modding API)
- **Plataforma:** Windows, Linux, ou Termux

## 🎮 Como Usar
1. Derrote o **Falso Cavaleiro**
2. Charm "Aliados Eternos" será desbloqueado
3. Equipe o charm no seu inventário
4. Mate inimigos → Eles viram aliados!

## 🔗 Links
- [Repositório GitHub](https://github.com/rihckeume/hollow-knight-mod-aliados)
- [Guia Completo](https://github.com/rihckeume/hollow-knight-mod-aliados/blob/main/GUIA_LANCAMENTO.md)
- [Hollow Knight Modding API](https://github.com/hk-modding/api)
- [Lumafly](https://themulhima.github.io/Lumafly/)

## 📄 Licença
MIT License - Veja LICENSE para detalhes.
```

- [ ] **Anexar Arquivo:**
  - [ ] Carregar `InimigosViramAliados.dll` (de `bin/Release/net472/`)
  - [ ] Carregar `Source Code (zip)` (código-fonte do release)

### Anúncio/Divulgação
- [ ] **Discord HK Modding:** Postar link da release
- [ ] **Nexus Mods (opcional):** Considerar publicar lá também
- [ ] **Wiki/Modlinks:** Adicionar à lista oficial de mods
- [ ] **Twitter/Social (opcional):** Compartilhar anúncio

---

## ✅ Testes Finais (Antes do Lançamento)

### Teste em Ambiente Limpo
1. [ ] **Criar VM ou usuário novo** (sem outros mods)
2. [ ] **Instalar Hollow Knight** (versão 1.5.78)
3. [ ] **Instalar Lumafly**
4. [ ] **Instalar mod via DLL anexada** (não via repositório local)
5. [ ] **Executar jogo**
6. [ ] **Verificar logs:** Mod carrega corretamente
7. [ ] **Testar funcionalidade:**
   - [ ] Derrota Falso Cavaleiro
   - [ ] Charm desbloqueado
   - [ ] Mata inimigo comum
   - [ ] Inimigo vira aliado
   - [ ] Aliado fica verde
   - [ ] Vizinhos recrutados
8. [ ] **Verificar performance:** Sem travamentos ou lag
9. [ ] **Salvar/carregar:** Jogo continua funcionando

### Teste de Compatibilidade
- [ ] **Sem mods:** Tudo funciona
- [ ] **Com mod popular (ex: Godseeker):** Sem conflitos
- [ ] **Com outro mod que modifica inimigos:** Sem crashes

---

## 🎯 Pós-Lançamento

### Monitoramento
- [ ] **Issues no GitHub:** Monitorar e responder
- [ ] **Discord/Comunidades:** Estar aberto para feedback
- [ ] **Logs de crash:** Se reportados, analisar e criar patch

### Manutenção
- [ ] **Atualizar se HK receber patch:** Adaptar mod
- [ ] **Atualizar Modding API:** Se nova versão disponível
- [ ] **Adicionar features:** Baseado em sugestões

### Documentação Viva
- [ ] **README:** Manter atualizado
- [ ] **CHANGELOG:** Adicionar novas versões
- [ ] **Wiki (se necessário):** Documentação avançada

---

## 📊 Status Geral

**Data de Criação:** 2026-07-18  
**Data de Lançamento:** ___________  
**Versão Lançada:** 1.1.0  
**Status:** ⬜ Não Iniciado | 🟨 Em Progresso | 🟩 Completo

---

**Assinatura:**  
Modder: `rihckeume`  
Data de Conclusão: ___________
