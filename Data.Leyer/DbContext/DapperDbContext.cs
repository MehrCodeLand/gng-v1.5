using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Leyer.DbContext;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _sqlConn;

    public DapperDbContext( IConfiguration configuration )
    {
        _configuration = configuration;
        _sqlConn = _configuration.GetConnectionString("SqlConn");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_sqlConn);

}
