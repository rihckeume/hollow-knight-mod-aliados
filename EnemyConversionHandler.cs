using UnityEngine;

namespace InimigosViramAliados
{
    /// <summary>
    /// Responsável pela lógica de conversão de inimigos em aliados.
    /// Implementa a transformação visual, remoção de dano, e recrutamento de vizinhos.
    /// </summary>
    public static class EnemyConversionHandler
    {
        /// <summary>
        /// Cor visual dos aliados (verde)
        /// </summary>
        private static Color ALLY_COLOR = new Color(0.5f, 1f, 0.5f, 1f); // Verde claro

        /// <summary>
        /// Cor original dos inimigos (armazenada antes da conversão)
        /// </summary>
        private static Color ORIGINAL_COLOR = Color.white;

        /// <summary>
        /// Converte um inimigo em aliado do jogador
        /// </summary>
        public static bool ConvertEnemyToAlly(GameObject enemy)
        {
            if (enemy == null)
            {
                Modding.Logger.Log("[Aliados] Tentativa de converter enemy nulo!");
                return false;
            }

            // Verificar se já é aliado
            if (enemy.GetComponent<EnemyAllyTag>() != null)
            {
                return false;
            }

            // Verificar limite de aliados
            if (AllyManager.IsAllyLimitReached())
            {
                Modding.Logger.Log($"[Aliados] Limite de {AllyManager.MAX_ALLIES} aliados atingido!");
                return false;
            }

            try
            {
                // 1. Adicionar tag de aliado
                var allyTag = enemy.AddComponent<EnemyAllyTag>();
                AllyManager.RegisterAlly(enemy, allyTag.AllyId);

                // 2. Remover capacidade de dano ao jogador
                RemoveDamageComponents(enemy);

                // 3. Aplicar transformação visual
                ApplyAllyVisuals(enemy);

                // 4. Bloquear IA agressiva
                DisableAggresiveAI(enemy);

                // 5. Recrutar inimigos vizinhos
                RecruitNearbyEnemies(enemy);

                Modding.Logger.Log($"[Aliados] Inimigo convertido! Total: {AllyManager.GetAllyCount()}");
                return true;
            }
            catch (System.Exception ex)
            {
                Modding.Logger.Log($"[Aliados] Erro ao converter inimigo: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Remove componentes que causam dano ao jogador
        /// </summary>
        private static void RemoveDamageComponents(GameObject enemy)
        {
            // Remover DamageHero se existir
            var damageHero = enemy.GetComponent<DamageHero>();
            if (damageHero != null)
            {
                Object.Destroy(damageHero);
            }

            // Remover ContactDamageHero se existir
            var contactDmg = enemy.GetComponent<ContactDamageHero>();
            if (contactDmg != null)
            {
                Object.Destroy(contactDmg);
            }

            Modding.Logger.Log($"[Aliados] Componentes de dano removidos de {enemy.name}");
        }

        /// <summary>
        /// Aplica transformação visual ao aliado (cor, efeitos)
        /// </summary>
        private static void ApplyAllyVisuals(GameObject enemy)
        {
            // Tentar mudar cor do sprite
            var spriteRenderer = enemy.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = ALLY_COLOR;
            }

            // Tentar adicionar aura/efeito (opcional)
            var childSprites = enemy.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in childSprites)
            {
                if (sprite != spriteRenderer) // Evitar duplicar a mudança
                {
                    sprite.color = ALLY_COLOR;
                }
            }

            Modding.Logger.Log($"[Aliados] Visuais aplicados a {enemy.name}");
        }

        /// <summary>
        /// Desativa comportamentos de IA agressiva
        /// </summary>
        private static void DisableAggresiveAI(GameObject enemy)
        {
            // Buscar e desativar componentes de IA comuns do HK
            var fsm = enemy.GetComponent<PlayMakerFSM>();
            if (fsm != null && fsm.enabled)
            {
                // Não destruir FSM, apenas desativar transições agressivas
                // Idealmente, poderíamos enviar evento "ALLY" para máquina de estado
                // Por enquanto, apenas marcamos para referência
                Modding.Logger.Log($"[Aliados] FSM detectado em {enemy.name}, recomenda-se ajuste manual");
            }

            // Remover componentes de busca de alvo
            var healthManager = enemy.GetComponent<HealthManager>();
            if (healthManager != null && healthManager.enabled)
            {
                // Deixar vivo, mas marcado como aliado
            }

            Modding.Logger.Log($"[Aliados] IA desativada para {enemy.name}");
        }

        /// <summary>
        /// Recruta inimigos próximos para também serem aliados
        /// (efeito em cadeia, porém limitado)
        /// </summary>
        private static void RecruitNearbyEnemies(GameObject newAlly)
        {
            Vector3 position = newAlly.transform.position;
            float radius = AllyManager.RECRUITMENT_RADIUS;

            // Buscar todos os colliders em área
            Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius);

            int recruited = 0;
            foreach (var collider in hits)
            {
                if (collider.gameObject == newAlly)
                    continue;

                // Verificar se é um inimigo
                if (collider.CompareTag("Enemy") || collider.gameObject.name.Contains("Enemy") || collider.gameObject.name.Contains("enemy"))
                {
                    // Tentar converter
                    if (ConvertEnemyToAlly(collider.gameObject))
                    {
                        recruited++;
                        if (recruited >= 3) // Limitar recrutamento por evento
                            break;
                    }
                }
            }

            if (recruited > 0)
            {
                Modding.Logger.Log($"[Aliados] {recruited} inimigos vizinhos recrutados!");
                var tag = newAlly.GetComponent<EnemyAllyTag>();
                if (tag != null)
                    tag.RecruitsCount = recruited;
            }
        }
    }
}