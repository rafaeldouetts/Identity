using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Services
{
    public interface IRedisService
    {
        Task SetValueAsync(string key, string value, TimeSpan expired);
        Task<string?> GetValueAsync(string key);
        Task<bool> IncrementLoginAttemptAsync(string email);
        Task ResetLoginAttemptsAsync(string email);
        Task<bool> IsLoginBlockedAsync(string email);
    }

    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task SetValueAsync(string key, string value, TimeSpan expired)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, value, expired);
        }

        public async Task<string?> GetValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }

        // Método para contar as tentativas de login
        public async Task<bool> IncrementLoginAttemptAsync(string email)
        {
            var db = _connectionMultiplexer.GetDatabase();

            // Nome da chave de tentativas de login
            var key = $"login_attempts:{email}";

            // Incrementa o número de tentativas (caso não exista, inicializa com 1)
            var attempts = await db.StringIncrementAsync(key);

            // Define o tempo de expiração de 5 minutos para essa chave
            await db.KeyExpireAsync(key, TimeSpan.FromMinutes(5));

            // Se o número de tentativas for maior que 3, bloqueia o login
            if (attempts >= 3)
            {
                await BlockLoginAsync(email); // Bloqueia o login
                return false; // Login bloqueado
            }

            return true; // Login permitido
        }

        // Método para bloquear o login por 30 minutos
        public async Task BlockLoginAsync(string email)
        {
            var db = _connectionMultiplexer.GetDatabase();

            // Nome da chave de bloqueio
            var key = $"login_blocked:{email}";

            // Define o valor da chave como true (indicando que está bloqueado)
            await db.StringSetAsync(key, "true");

            // Define o tempo de expiração de 30 minutos para essa chave
            await db.KeyExpireAsync(key, TimeSpan.FromMinutes(30));
        }

        // Método para verificar se o login está bloqueado
        public async Task<bool> IsLoginBlockedAsync(string email)
        {
            var db = _connectionMultiplexer.GetDatabase();

            // Nome da chave de bloqueio
            var key = $"login_blocked:{email}";

            // Verifica se a chave de bloqueio existe
            var isBlocked = await db.StringGetAsync(key);

            // Se o valor for "true", o login está bloqueado
            return isBlocked == "true";
        }

        // Método para resetar as tentativas de login
        public async Task ResetLoginAttemptsAsync(string email)
        {
            var db = _connectionMultiplexer.GetDatabase();

            // Nome da chave de tentativas de login
            var key = $"login_attempts:{email}";

            // Apaga a chave de tentativas
            await db.KeyDeleteAsync(key);
        }
    }
}
