using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Живой_Уголок
{
    public static class ApiClient
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000"),
            Timeout = TimeSpan.FromSeconds(10)
        };

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static int? CurrentPersonId { get; set; }
        public static string CurrentPersonName { get; set; } = "";

        public static async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode) return default;
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, jsonOptions);
            }
            catch
            {
                return default;
            }
        }

        public static async Task<(bool Success, T? Data, string Error)> PostAsync<T>(string endpoint, object payload)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, content);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<T>(json, jsonOptions);
                    return (true, data, string.Empty);
                }
                else
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(json);
                        if (doc.RootElement.TryGetProperty("error", out var errElem))
                            return (false, default, errElem.GetString() ?? "Помилка сервера");
                    }
                    catch { }
                    return (false, default, $"Помилка: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return (false, default, ex.Message);
            }
        }
    }

    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string BirthDate { get; set; } = "";
        public string Status { get; set; } = "Alive";
        public int Balance { get; set; }
        public string? DeathDate { get; set; }
        public int? TierId { get; set; }
        public int? MorgueId { get; set; }
        public string? BuriedAt { get; set; }
    }

    public class DeadRowDto
    {
        public string Name { get; set; } = "";
        public string BirthDate { get; set; } = "";
        public string? DeathDate { get; set; }
        public string Tier { get; set; } = "";
    }

    public class WaitingRowDto
    {
        public int PersonId { get; set; }
        public string Morgue { get; set; } = "";
        public string Name { get; set; } = "";
        public string? DeathDate { get; set; }
        public string Tier { get; set; } = "";
    }

    public class OrderRowDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string BirthDate { get; set; } = "";
        public string Coffin { get; set; } = "";
        public string Tier { get; set; } = "";
        public string Priest { get; set; } = "";
        public string Status { get; set; } = "";
    }

    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Price { get; set; }
    }

    public class ShopResponseDto
    {
        public List<ItemDto> Coffins { get; set; } = new();
        public List<ItemDto> Tiers { get; set; } = new();
        public List<ItemDto> Priests { get; set; } = new();
    }

    public class StartGameResponseDto
    {
        public int GameId { get; set; }
        public int GridSize { get; set; }
        public int BombCount { get; set; }
    }

    public class DigResponseDto
    {
        public bool Exploded { get; set; }
        public bool Finished { get; set; }
        public int Reward { get; set; }
        public int CurrentReward { get; set; }
        public List<int>? Bombs { get; set; }
        public List<int>? Dug { get; set; }
        public string? Message { get; set; }
    }

    public class GenericResponse
    {
        public bool Buried { get; set; }
        public int Balance { get; set; }
        public int Spent { get; set; }
        public bool Dead { get; set; }
        public string? Tier { get; set; }
    }
}
