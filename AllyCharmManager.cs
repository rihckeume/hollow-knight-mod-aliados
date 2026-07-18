using Modding;

namespace InimigosViramAliados
{
    /// <summary>
    /// Charm/Amuleto que ativa o poder de converter inimigos em aliados.
    /// Obtido ao derrotar o Falso Cavaleiro.
    /// </summary>
    public static class AllyCharmManager
    {
        /// <summary>
        /// Nome do charm no jogo
        /// </summary>
        public const string CHARM_NAME = "Aliados Eternos";

        /// <summary>
        /// ID único do charm (customizado)
        /// </summary>
        public const int CHARM_ID = 999;

        /// <summary>
        /// Descrição do charm
        /// </summary>
        public const string CHARM_DESCRIPTION = "Um antigo amuleto que permite controlar os inimigos derrotados.\n\nAo ativar: Inimigos mortos se tornam seus aliados e recrutam vizinhos.";

        /// <summary>
        /// Custo em Mask Shards
        /// </summary>
        public const int CHARM_COST = 3;

        /// <summary>
        /// Verifica se o charm está equipado
        /// </summary>
        public static bool IsCharmEquipped()
        {
            try
            {
                // Tentar obter dados do charm
                if (PlayerData.instance == null)
                {
                    Logger.Log("[Aliados] ⚠ PlayerData é nulo!");
                    return false;
                }

                return PlayerData.instance.GetBool("equippedCharm_" + CHARM_ID);
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ⚠ Erro ao verificar charm equipado: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Ativa o charm no inventário do jogador
        /// </summary>
        public static void ActivateCharm()
        {
            try
            {
                if (PlayerData.instance == null)
                {
                    Logger.Log("[Aliados] ⚠ PlayerData é nulo - não é possível ativar charm!");
                    return;
                }

                // Marcar charm como obtido
                PlayerData.instance.SetBool("gotCharm_" + CHARM_ID, true);
                
                // Incrementar contador de charms obtidos
                int charmCount = PlayerData.instance.GetInt("charmsOwned");
                PlayerData.instance.SetInt("charmsOwned", charmCount + 1);

                Logger.Log($"[Aliados] ✨ Charm '{CHARM_NAME}' desbloqueado!");
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ❌ Erro ao ativar charm: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém o status do charm
        /// </summary>
        public static string GetCharmStatus()
        {
            try
            {
                if (PlayerData.instance == null)
                    return "⚠ PlayerData indisponível";

                bool hasCharm = PlayerData.instance.GetBool("gotCharm_" + CHARM_ID);
                bool isEquipped = IsCharmEquipped();

                if (!hasCharm)
                    return "🔒 Bloqueado";
                else if (isEquipped)
                    return "✅ Equipado";
                else
                    return "📦 No inventário";
            }
            catch (System.Exception ex)
            {
                Logger.Log($"[Aliados] ⚠ Erro ao obter status do charm: {ex.Message}");
                return "❌ Erro";
            }
        }
    }
}
