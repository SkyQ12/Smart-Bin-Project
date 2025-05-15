using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SmartBin.Infrastructure.Domain.Models.Bin;
using SmartBin.Infrastructure.MqttClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Repositories.ErrorHistories
{
    public class ErrorHistoryRepository:  BaseRepository, IErrorHistoryRepository
    {
        public ErrorHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<ErrorHistory>> GetErrorHistoryByBinUnitId(string binUnitId)
        {
            return await _context.ErrorHistories
                                 .Where(x => x.BinUnitId == binUnitId)
                                 .ToListAsync();
        }

        public async Task<List<ErrorHistory>> GetErrorHistoriesByDateTime(DateTime timeStamp)
        {
            return await _context.ErrorHistories
                                 .Where(x => x.TimeStamp == timeStamp)
                                 .ToListAsync();
        }

        public async Task<List<ErrorHistoryViewModel>> GetErrorHistoriesFromDateTime1ToDateTime2(DateTime timeStamp1, DateTime timeStamp2)
        {
            return await _context.ErrorHistories
                                 .Where(x => x.TimeStamp >= timeStamp1 && x.TimeStamp <= timeStamp2)
                                 .Select(x => new ErrorHistoryViewModel
                                 {
                                     BinUnitId = x.BinUnitId,
                                     Id = x.Id,
                                     ErrorId = x.ErrorId,
                                     TimeStamp = x.TimeStamp
                                 })
                                 .ToListAsync();
        }

    public async Task<bool> DeleteErrorHistoriesByBinUnitId(string binUnitId)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                // Sử dụng tham số trong câu lệnh SQL thay vì giá trị cố định
                string query = @"DELETE FROM ErrorHistories WHERE BinUnitId = @BinUnitId;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@BinUnitId", binUnitId);  // Giá trị BinUnitId từ tham số của hàm

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
            }
            return true; // Trả về true nếu xóa thành công
        }

        public async Task<bool> DeleteErrorHistoriesFromDateTime1ToDateTime2(DateTime timeStamp1, DateTime timeStamp2)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                // Sử dụng tham số trong câu lệnh SQL thay vì giá trị cố định
                string query = @"DELETE FROM ErrorHistories WHERE TimeStamp >= @TimeStamp1 AND TimeStamp <= @TimeStamp2;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@TimeStamp1", timeStamp1);  // Giá trị TimeStamp1 từ tham số của hàm
                    cmd.Parameters.AddWithValue("@TimeStamp2", timeStamp2);  // Giá trị TimeStamp2 từ tham số của hàm

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
            }
            return true; // Trả về true nếu xóa thành công
        }

        public async Task SaveErrorHistoryToDatabase(int id, string binUnitId, int errorId, DateTime timeStamp)
        {
            string connectionString = GetConnectionStringFromConfig();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                // Sử dụng tham số trong câu lệnh SQL thay vì giá trị cố định
                string query = @"INSERT INTO ErrorHistories (Id, BinUnitId, ErrorId, TimeStamp)
                VALUES (@Id, @BinUnitId, @ErrorId, @TimeStamp);";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@Id", id);  // Giá trị BinId từ tham số của hàm
                    cmd.Parameters.AddWithValue("@BinUnitId", binUnitId);  // Giá trị BinUnitId từ tham số của hàm
                    cmd.Parameters.AddWithValue("@ErrorId", errorId);  // Giá trị ErrorId từ tham số của hàm
                    cmd.Parameters.AddWithValue("@TimeStamp", timeStamp);  // Giá trị TimeStamp từ tham số của hàm

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine(rowsAffected > 0 ? "✅ Database updated successfully!" : "⚠️ No records updated.");
                }
            }
            Console.WriteLine("🔄 ErrorHistory SaveChanges called."); // Logging to indicate function completion
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
