using Modding;
using UnityEngine;

namespace InimigosViramAliados
{
    /// <summary>
    /// Mod "Aliados de Inimigos" para Hollow Knight v1.5.78.11838+
    /// 
    /// Funcionalidade: Cada inimigo derrotado se torna aliado do jogador.
    /// Inimigos próximos também são recrutados automaticamente.
    /// 
    /// Autor: Gerado por Copilot
    /// Versão: 1.0.0
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
            return "1.0.0";
        }

        /// <summary>
        /// Inicializa o mod e subscreve aos hooks necessários
        /// </summary>
        public override void Initialize()
        {
            Logger.Log("[Aliados] Inicializando mod...");

            // Hook de morte de inimigo - evento principal
            ModHooks.OnRecieveDeathEventHook += OnEnemyDeath;

            // Hook de carregamento de cena - limpar aliados da cena anterior
            ModHooks.BeforeSceneLoadHook += OnBeforeSceneLoad;

            // Hook de carregamento completo de cena
            ModHooks.SceneLoadHook += OnSceneLoaded;

            Logger.Log("[Aliados] Mod inicializado com sucesso!");
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
            // Evitar duplicação de eventos
            if (eventAlreadyRecieved)
            {
                return;
            }

            if (enemyDeathEffects == null)
            {
                Logger.Log("[Aliados] EnemyDeathEffects nulo!");
                return;
            }

            GameObject enemy = enemyDeathEffects.gameObject;
            if (enemy == null)
            {
                Logger.Log("[Aliados] Enemy gameObject nulo!");
                return;
            }

            Logger.Log($"[Aliados] Inimigo morrendo: {enemy.name}");

            // Converter para aliado
            bool converted = EnemyConversionHandler.ConvertEnemyToAlly(enemy);

            if (converted)
            {
                // Deixar o inimigo "vivo" como aliado
                // Não permitir destruição imediata
                resetDeathEvent = true;

                // Log
                AllyManager.LogAllyStatus();
            }
        }

        /// <summary>
        /// Hook chamado antes de carregar uma cena.
        /// Limpa aliados da cena anterior para evitar memory leak.
        /// </summary>
        private void OnBeforeSceneLoad(string nextSceneName)
        {
            Logger.Log($"[Aliados] Carregando cena: {nextSceneName}");
            AllyManager.ClearAllAllies();
        }

        /// <summary>
        /// Hook chamado após cena ser totalmente carregada.
        /// Pode ser usado para inicializações da cena.
        /// </summary>
        private void OnSceneLoaded(Scene scene)
        {
            Logger.Log($"[Aliados] Cena carregada: {scene.name}. Aliados resetados.");
        }

        /// <summary>
        /// Destruição/limpeza do mod
        /// </summary>
        public override void Unload()
        {
            Logger.Log("[Aliados] Descarregando mod...");

            // Desinscrever de hooks
            ModHooks.OnRecieveDeathEventHook -= OnEnemyDeath;
            ModHooks.BeforeSceneLoadHook -= OnBeforeSceneLoad;
            ModHooks.SceneLoadHook -= OnSceneLoaded;

            // Limpar aliados
            AllyManager.ClearAllAllies();

            Logger.Log("[Aliados] Mod descarregado.");
        }
    }
}