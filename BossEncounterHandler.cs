using Modding;
using UnityEngine;

namespace InimigosViramAliados
{
    /// <summary>
    /// Gerencia o encontro e derrota do Falso Cavaleiro.
    /// Ao vencer, o jogador obtém o charm "Aliados Eternos".
    /// </summary>
    public static class BossEncounterHandler
    {
        /// <summary>
        /// Nome do boss que ativa o charm
        /// </summary>
        public const string BOSS_NAME = "False Knight";

        /// <summary>
        /// Variável de controle para evitar múltiplas ativações
        /// </summary>
        private static bool _hookSubscribed = false;

        /// <summary>
        /// Hook para detectar derrota do Falso Cavaleiro
        /// </summary>
        public static void Initialize()
        {
            try
            {
                if (_hookSubscribed)
                {
                    Logger.Log("[Aliados] ⚠ BossEncounterHandler já foi inicializado!");
                    return;
                }

                // Hook quando o boss é derrotado
                ModHooks.OnRecieveDeathEventHook += OnBossDeath;
                _hookSubscribed = true;

                Logger.Log("[Aliados] ✓ Boss encounter handler inicializado");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ❌ Erro ao inicializar boss handler: {ex.Message}");
            }
        }

        /// <summary>
        /// Chamado quando qualquer inimigo morre - verifica se é o False Knight
        /// </summary>
        private static void OnBossDeath(
            EnemyDeathEffects enemyDeathEffects,
            bool eventAlreadyRecieved,
            ref float? attackDirection,
            ref bool resetDeathEvent,
            ref bool spellBurn,
            ref bool isWatery)
        {
            if (eventAlreadyRecieved || enemyDeathEffects == null)
                return;

            try
            {
                GameObject enemy = enemyDeathEffects.gameObject;
                if (enemy == null)
                    return;

                // Verificar se é o Falso Cavaleiro
                string enemyName = enemy.name;
                bool isFalseKnight = enemyName.Contains("False") || 
                                     enemyName.Contains("Knight") || 
                                     enemyName.Contains("Infected");

                if (isFalseKnight)
                {
                    OnFalseKnightDefeated();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ⚠ Erro no OnBossDeath: {ex.Message}");
            }
        }

        /// <summary>
        /// Executado quando o Falso Cavaleiro é derrotado
        /// </summary>
        private static void OnFalseKnightDefeated()
        {
            try
            {
                Logger.Log("[Aliados] ════════════════════════════════════════");
                Logger.Log("[Aliados] 🏆 FALSO CAVALEIRO DERROTADO!");
                Logger.Log("[Aliados] ════════════════════════════════════════");

                // Ativar o charm
                AllyCharmManager.ActivateCharm();

                // Exibir mensagem
                ShowCharmObtainedMessage();

                Logger.Log("[Aliados] ✨ Charm 'Aliados Eternos' desbloqueado!");
                Logger.Log("[Aliados] 📍 Você agora pode equipar o charm no seu inventário!");
                Logger.Log("[Aliados] ════════════════════════════════════════");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ❌ Erro ao processar derrota do boss: {ex.Message}");
            }
        }

        /// <summary>
        /// Exibe mensagem visual no jogo quando charm é obtido
        /// </summary>
        private static void ShowCharmObtainedMessage()
        {
            try
            {
                Logger.Log("[Aliados] 🎁 Charm obtido: Aliados Eternos");
                Logger.Log("[Aliados] 📖 Um antigo amuleto que permite controlar inimigos derrotados");
            }
            catch
            {
                // Silent fail
            }
        }

        /// <summary>
        /// Limpeza ao descarregar o mod
        /// </summary>
        public static void Cleanup()
        {
            try
            {
                if (!_hookSubscribed)
                    return;

                ModHooks.OnRecieveDeathEventHook -= OnBossDeath;
                _hookSubscribed = false;

                Logger.Log("[Aliados] ✓ Boss encounter handler limpo");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ⚠ Erro ao limpar boss handler: {ex.Message}");
            }
        }
    }
}
