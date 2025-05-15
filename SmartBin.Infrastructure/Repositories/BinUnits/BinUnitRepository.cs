
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;

namespace SmartBin.Infrastructure.Repositories.BinUnits
{
    public class BinUnitRepository : BaseRepository, IBinUnitRepository
    {
        public BinUnitRepository(ApplicationDbContext context) : base(context)
        {
        }

        
        public async Task AddCollectedHistoryAsync(CollectedHistory history)
        {
            await _context.CollectedHistories.AddAsync(history);
        }
        
        public async Task AddErrorHistoryAsync(ErrorHistory history)
        {
            await _context.ErrorHistories.AddAsync(history);
        }

        public async Task<BinUnit> GetBinUnitByIdAsync(string id)
        {
            // Dùng Include để lấy luôn các CollectedHistories liên quan tới BinUnitId
            var binUnit = await _context.BinUnits
                                        .Include(x => x.CollectedHistories)
                                        .FirstOrDefaultAsync(x => x.BinUnitId == id);

            return binUnit != null ? binUnit : throw new ResourceNotfoundException("Not found binUnit");
        }


        public async Task<bool> IsExistBinUnit(string id)
        {
            return await _context.BinUnits.AnyAsync(x => x.BinUnitId == id);
        }

        public async Task SaveMetricsToBinUnitDatabase(string binUnitId, string metricType, object value)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Mở kết nối cơ sở dữ liệu
                    await conn.OpenAsync();
                    //Console.WriteLine("🔗 Database connection opened.");

                    // Câu lệnh SQL để cập nhật thông tin
                    string query = @"
                UPDATE BinUnits SET 
                    Level = CASE WHEN @MetricType = 'Level' THEN @Value ELSE Level END,
                    Fault = CASE WHEN @MetricType = 'Fault' THEN @Value ELSE Fault END,
                    CompressCnt = CASE WHEN @MetricType = 'CompressCnt' THEN @Value ELSE CompressCnt END,
                    FullCnt = CASE WHEN @MetricType = 'FullCnt' THEN @Value ELSE FullCnt END,
                    Status = CASE WHEN @MetricType = 'Status' THEN @Value ELSE Status END,
                    Flame = CASE WHEN @MetricType = 'Flame' THEN @Value ELSE Flame END,
                    Vibration = CASE WHEN @MetricType = 'Vibration' THEN @Value ELSE Vibration END
                WHERE BinUnitId = @BinUnitId;";

                    // Kiểm tra và chuyển đổi giá trị 'value' sang kiểu int nếu có thể
                    Object LoadValue;
                    int intValue;
                    if (!int.TryParse(value.ToString(), out intValue))
                    {
                        LoadValue = value.ToString();
                    }
                    else
                    {
                        LoadValue = intValue;
                    }
                    // Thực thi câu lệnh SQL
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MetricType", metricType);
                        cmd.Parameters.AddWithValue("@Value", LoadValue);
                        cmd.Parameters.AddWithValue("@BinUnitId", binUnitId);

                        // Thực thi câu lệnh và kiểm tra số dòng bị ảnh hưởng
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        //Console.WriteLine($"Rows affected: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            //Console.WriteLine("✅ Database updated successfully!");
                        }
                        else
                        {
                            //Console.WriteLine("⚠️ No records updated.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"⚠️ An error occurred: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                    //Console.WriteLine("🔒 Database connection closed.");
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
