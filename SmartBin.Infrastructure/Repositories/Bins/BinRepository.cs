
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartBin.Infrastructure.Domain.Models.Bin;

namespace SmartBin.Infrastructure.Repositories.Bins
{
    public class BinRepository : BaseRepository, IBinRepository
    {
        public BinRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Bin> CreateNewBinAsync(Bin bin)
        {
            var newBin = await _context.Bins.AddAsync(bin);
            return newBin.Entity;
        }
        public async Task CreateNewBinUnitsAsync(List<BinUnit> binUnits)
        {
            await _context.BinUnits.AddRangeAsync(binUnits);
        }

        public async Task DeleteBinById(Bin bin)
        {
            _context.Bins.Remove(bin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bin>> GetAllBinsAsync()
        {
            return await _context.Bins.Include(x => x.BinUnits).ThenInclude(x => x.CollectedHistories).ToListAsync();
        }

        public async Task<Bin> GetBinByIdAsync(string id)
        {
            var bin = await _context.Bins
                .Include(x => x.BinUnits) // Lấy các BinUnits của Bin
                    .ThenInclude(bu => bu.CollectedHistories) // Lấy các CollectedHistories của BinUnit
                .FirstOrDefaultAsync(x => x.Id == id);

            return bin != null ? bin : throw new ResourceNotfoundException("Not found bin!");
        }


        public async Task<bool> IsBinAlreadyExist(string id)
        {
            return await _context.Bins.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateBinAsync(Bin bin)
        {
            _context.Bins.Update(bin);
            await _context.SaveChangesAsync();
        }

        
        public async Task DeleteQRByBinId(string binId)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    Console.WriteLine("🔗 Database connection opened.");

                    string query = @"Update Bins SET QR = NULL WHERE Id = @BinId;";

                    // Thực thi câu lệnh SQL
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BinId", binId);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        /*Console.WriteLine(rowsAffected > 0
                            ? "✅ QR deleted successfully!"
                            : "⚠️ No records deleted.");*/
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"❌ Error deleting QR: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                    //Console.WriteLine("🔒 Database connection closed.");
                }
            }
        }

        public async Task DeleteQRByQR(string binId, string qrToDelete)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    Console.WriteLine("🔗 Database connection opened.");

                    // 1️⃣ Bước 1: Lấy dữ liệu QR hiện tại
                    string selectQuery = @"SELECT QR FROM Bins WHERE Id = @BinId";
                    string currentQRJson = null;

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@BinId", binId);
                        var result = await selectCmd.ExecuteScalarAsync();
                        currentQRJson = result != null && result != DBNull.Value ? result.ToString() : null;
                        Console.WriteLine($"🔍 Result from DB: {result ?? "null"}");

                    }

                    if (string.IsNullOrEmpty(currentQRJson))
                    {
                        Console.WriteLine("⚠️ No QR data found for this bin.");
                        return;
                    }

                    // 2️⃣ Bước 2: Phân tích JSON và xóa mã QR cần xoá
                    var qrList = System.Text.Json.JsonSerializer.Deserialize<List<string>>(currentQRJson);
                    qrList.RemoveAll(qr => qr == qrToDelete);

                    // 3️⃣ Bước 3: Nếu còn QR => lưu lại, nếu không => set NULL
                    string newQRJson = qrList.Count > 0 ? System.Text.Json.JsonSerializer.Serialize(qrList) : null;

                    // 4️⃣ Bước 4: Cập nhật ngược lại vào DB
                    string updateQuery = @"UPDATE Bins SET QR = @NewQR WHERE Id = @BinId";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@NewQR", (object)newQRJson ?? DBNull.Value);
                        updateCmd.Parameters.AddWithValue("@BinId", binId);

                        int rowsAffected = await updateCmd.ExecuteNonQueryAsync();
                        Console.WriteLine(rowsAffected > 0
                            ? "✅ QR updated successfully!"
                            : "⚠️ No records updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                    Console.WriteLine("🔒 Database connection closed.");
                }
            }
        }





        public async Task SaveMetricsToBinDatabase(string binId, string metricType, object value)
        {

            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    //Console.WriteLine("🔗 Database connection opened.");
                    string query = @"
                UPDATE Bins SET 
                    Battery = CASE WHEN @MetricType = 'Battery' THEN @BatteryValue ELSE Battery END,
                    Internet = CASE WHEN @MetricType = 'Internet' THEN @InternetValue ELSE Internet END,
                    QR = CASE WHEN @MetricType = 'QR' THEN @QRValue ELSE QR END
                WHERE Id = @BinId;
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm params
                        cmd.Parameters.AddWithValue("@MetricType", metricType);
                        cmd.Parameters.AddWithValue("@BinId", binId);

                        // Thiết lập giá trị tùy theo metricType
                        if (metricType == "Battery")
                        {
                            cmd.Parameters.AddWithValue("@BatteryValue", Convert.ToInt32(value));
                            cmd.Parameters.AddWithValue("@InternetValue", DBNull.Value);
                            cmd.Parameters.AddWithValue("@QRValue", DBNull.Value);
                        }
                        else if (metricType == "Internet")
                        {
                            cmd.Parameters.AddWithValue("@BatteryValue", DBNull.Value);
                            cmd.Parameters.AddWithValue("@InternetValue", Convert.ToInt32(value));
                            cmd.Parameters.AddWithValue("@QRValue", DBNull.Value);
                        }
                        else if (metricType == "QR")
                        {
                            cmd.Parameters.AddWithValue("@BatteryValue", DBNull.Value);
                            cmd.Parameters.AddWithValue("@InternetValue", DBNull.Value);
                            cmd.Parameters.AddWithValue("@QRValue", value.ToString());
                        }
                        else
                        {
                            //Console.WriteLine($"⚠️ MetricType '{metricType}' không hợp lệ.");
                            return;
                        }

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        /*Console.WriteLine(rowsAffected > 0
                            ? "✅ Database updated successfully!"
                            : "⚠️ No records updated.");*/
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"❌ Error updating database: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                    //Console.WriteLine("🔒 Database connection closed.");
                }
            }

            //Console.WriteLine("Bin SaveChanges called.");
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
