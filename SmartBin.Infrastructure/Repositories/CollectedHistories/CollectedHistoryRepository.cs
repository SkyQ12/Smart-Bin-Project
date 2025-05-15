
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SmartBin.Infrastructure.Repositories.CollectedHistories
{
    public class CollectedHistoryRepository: BaseRepository, ICollectedHistoryRepository
    {
        public CollectedHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<CollectedHistory> CreateCollectedHistory(CollectedHistory historyEntity)
        {
            var newHistory = await _context.CollectedHistories.AddAsync(historyEntity);
            return newHistory.Entity;
        
        }
        public async Task UpdateCollectedHistoryAsync(CollectedHistory collectedHistory)
        {
            _context.CollectedHistories.Update(collectedHistory);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteCollectedHistoriesByBinUnitId(string binUnitId)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                // Sử dụng tham số trong câu lệnh SQL thay vì giá trị cố định
                string query = @"DELETE FROM CollectedHistories WHERE BinUnitId = @BinUnitId;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@BinUnitId", binUnitId);  // Giá trị BinUnitId từ tham số của hàm

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
        public async Task<bool> DeleteCollectedHistoriesFromDateTime1ToDateTime2(DateTime collectedTime1, DateTime collectedTime2)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                // Sử dụng tham số trong câu lệnh SQL thay vì giá trị cố định
                string query = @"DELETE FROM CollectedHistories WHERE CollectedTime >= @CollectedTime1 AND CollectedTime <= @CollectedTime2;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@CollectedTime1", collectedTime1);
                    cmd.Parameters.AddWithValue("@CollectedTime2", collectedTime2);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
        public async Task<bool> DeleteAll()
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                // Sử dụng tham số trong câu lệnh SQL thay vì giá trị cố định
                string query = @"DELETE FROM CollectedHistories;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        private static string GetConnectionStringFromConfig()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
