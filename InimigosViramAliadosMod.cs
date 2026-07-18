using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InimigosViramAliados
{
    /// <summary>
    /// Mod "Aliados de Inimigos" para Hollow Knight v1.5.78.11838+
    /// 
    /// Funcionalidade: Cada inimigo derrotado se torna aliado do jogador.
    /// Inimigos próximos também são recrutados automaticamente.
    /// 
    /// Autor: Gerado por Copilot
    /// Versão: 1.0.1 (Debugged)
    /// </summary>
    public class InimigosViramAliadosMod : Mod
    {
        /// <summary>
        /// Nome único do mod (usado pelo Modding API)
        /// </summary>
        public override string GetName()
        {
            return "Aliados de Inimigos";
        }

        /// <summary>
        /// Versão do mod
        /// </summary>
        public override string GetVersion()
        {
            return "1.0.1";
        }

        /// <summary>
        /// Inicializa o mod e subscreve aos hooks necessários
        /// </summary>
        public override void Initialize()
        {
            try
            {
                Logger.Log("[Aliados] ════════════════════════════════════════════");
                Logger.Log("[Aliados] 🎮 Inicializando Mod 'Aliados de Inimigos' v1.0.1");
                Logger.Log("[Aliados] ════════════════════════════════════════════");

                // Hook de morte de inimigo - evento principal
                ModHooks.OnRecieveDeathEventHook += OnEnemyDeath;
                Logger.Log("[Aliados] ✓ Hook OnRecieveDeathEventHook inscrito");

                // Hook de carregamento de cena - limpar aliados da cena anterior
                ModHooks.BeforeSceneLoadHook += OnBeforeSceneLoad;
                Logger.Log("[Aliados] ✓ Hook BeforeSceneLoadHook inscrito");

                // Hook de carregamento completo de cena
                ModHooks.SceneLoadHook += OnSceneLoaded;
                Logger.Log("[Aliados] ✓ Hook SceneLoadHook inscrito");

                Logger.Log("[Aliados] ════════════════════════════════════════════");
                Logger.Log("[Aliados] ✅ Mod inicializado com sucesso!");
                Logger.Log("[Aliados] ════════════════════════════════════════════");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ❌ ERRO na inicialização: {ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Hook chamado quando um inimigo morre.
        /// Responsável por converter o inimigo em aliado.
        /// </summary>
        private void OnEnemyDeath(
            EnemyDeathEffects enemyDeathEffects,
            bool eventAlreadyRecieved,
            ref float? attackDirection,
            ref bool resetDeathEvent,
            ref bool spellBurn,
            ref bool isWatery)
        {
            try
            {
                // Evitar duplicação de eventos
                if (eventAlreadyRecieved)
                {
                    return;
                }

                if (enemyDeathEffects == null)
                {
                    Logger.Log("[Aliados] ⚠ EnemyDeathEffects é nulo!");
                    return;
                }

                GameObject enemy = enemyDeathEffects.gameObject;
                if (enemy == null)
                {
                    Logger.Log("[Aliados] ⚠ Enemy gameObject é nulo!");
                    return;
                }

                Logger.Log($"[Aliados] 💀 Evento de morte: {enemy.name}");

                // Converter para aliado
                bool converted = EnemyConversionHandler.ConvertEnemyToAlly(enemy);

                if (converted)
                {
                    // Deixar o inimigo "vivo" como aliado
                    // Não permitir destruição imediata
                    resetDeathEvent = true;
                    Logger.Log($"[Aliados] ✓ {enemy.name} virou aliado!");

                    // Log de status
                    AllyManager.LogAllyStatus();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ❌ Erro em OnEnemyDeath: {ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Hook chamado antes de carregar uma cena.
        /// Limpa aliados da cena anterior para evitar memory leak.
        /// </summary>
        private void OnBeforeSceneLoad(string nextSceneName)
        {
            try
            {
                Logger.Log($"[Aliados] 🔄 Mudando cena para: {nextSceneName}");
                AllyManager.ClearAllAllies();
                Logger.Log("[Aliados] → Aliados antigos limpos da memória");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ⚠ Erro em OnBeforeSceneLoad: {ex.Message}");
            }
        }

        /// <summary>
        /// Hook chamado após cena ser totalmente carregada.
        /// Pode ser usado para inicializações da cena.
        /// </summary>
        private void OnSceneLoaded(Scene scene)
        {
            try
            {
                Logger.Log($"[Aliados] ✅ Cena carregada! ID: {scene.buildIndex}");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ⚠ Erro em OnSceneLoaded: {ex.Message}");
            }
        }

        /// <summary>
        /// Destruição/limpeza do mod
        /// </summary>
        public override void Unload()
        {
            try
            {
                Logger.Log("[Aliados] ════════════════════════════════════════════");
                Logger.Log("[Aliados] 🛑 Descarregando mod...");

                // Desinscrever de hooks
                ModHooks.OnRecieveDeathEventHook -= OnEnemyDeath;
                ModHooks.BeforeSceneLoadHook -= OnBeforeSceneLoad;
                ModHooks.SceneLoadHook -= OnSceneLoaded;

                Logger.Log("[Aliados] ✓ Hooks desinscritos");

                // Limpar aliados
                AllyManager.ClearAllAllies();
                Logger.Log("[Aliados] ✓ Aliados limpos");

                Logger.Log("[Aliados] ✅ Mod descarregado com sucesso!");
                Logger.Log("[Aliados] ════════════════════════════════════════════");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ❌ Erro ao descarregar: {ex.Message}");
            }
        }
    }
}
