using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    //[Authorize]
    [RoutePrefix("api/boleto")]
    public class BoletoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Boleto boleto = new Boleto();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT BOL_ID
                                                                    ,USU_CODIGO
                                                                    ,VUE_ID
                                                                    ,PAGO_ID
                                                                    ,BOL_FEC_IDA
                                                                    ,BOL_FEC_VUELTA
                                                                    ,BOL_PRECIO
                                                                    ,BOL_ASIENTO
                                                                    ,BOL_TERMINAL FROM BOLETO
                                                            WHERE BOL_ID = @BOL_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@BOL_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        boleto.BOL_ID = sqlDataReader.GetInt32(0);
                        boleto.USU_CODIGO = sqlDataReader.GetInt32(1);
                        boleto.VUE_ID = sqlDataReader.GetInt32(2);
                        boleto.PAGO_ID = sqlDataReader.GetInt32(3);
                        boleto.BOL_FEC_IDA = sqlDataReader.GetDateTime(4);
                        boleto.BOL_FEC_VUELTA = sqlDataReader.GetDateTime(5);
                        boleto.BOL_PRECIO = sqlDataReader.GetDecimal(6);
                        boleto.BOL_ASIENTO = sqlDataReader.GetString(7);
                        boleto.BOL_TERMINAL = sqlDataReader.GetString(8);
                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(boleto);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Boleto> boletos = new List<Boleto>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT BOL_ID
                                                                    ,USU_CODIGO
                                                                    ,VUE_ID
                                                                    ,PAGO_ID
                                                                    ,BOL_FEC_IDA
                                                                    ,BOL_FEC_VUELTA
                                                                    ,BOL_PRECIO
                                                                    ,BOL_ASIENTO
                                                                    ,BOL_TERMINAL FROM BOLETO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Boleto boleto = new Boleto()
                        {
                            BOL_ID = sqlDataReader.GetInt32(0),
                            USU_CODIGO = sqlDataReader.GetInt32(1),
                            VUE_ID = sqlDataReader.GetInt32(2),
                            PAGO_ID = sqlDataReader.GetInt32(3),
                            BOL_FEC_IDA = sqlDataReader.GetDateTime(4),
                            BOL_FEC_VUELTA = sqlDataReader.GetDateTime(5),
                            BOL_PRECIO = sqlDataReader.GetDecimal(6),
                            BOL_ASIENTO = sqlDataReader.GetString(7),
                            BOL_TERMINAL = sqlDataReader.GetString(8)
                        };

                        boletos.Add(boleto);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(boletos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Boleto boleto)
        {
            if (boleto == null)
                return BadRequest();

            if (RegistrarBoleto(boleto))
                return Ok(boleto);
            else
                return InternalServerError();
        }

        private bool RegistrarBoleto(Boleto boleto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO BOLETO (USU_CODIGO
                                                                            ,VUE_ID
                                                                            ,PAGO_ID
                                                                            ,BOL_FEC_IDA
                                                                            ,BOL_FEC_VUELTA
                                                                            ,BOL_PRECIO
                                                                            ,BOL_ASIENTO
                                                                            ,BOL_TERMINAL) 
                                                            VALUES (@USU_CODIGO
                                                                    ,@VUE_ID
                                                                    ,@PAGO_ID
                                                                    ,@BOL_FEC_IDA
                                                                    ,@BOL_FEC_VUELTA
                                                                    ,@BOL_PRECIO
                                                                    ,@BOL_ASIENTO
                                                                    ,@BOL_TERMINAL)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@USU_CODIGO", boleto.USU_CODIGO);
                sqlCommand.Parameters.AddWithValue("@VUE_ID", boleto.VUE_ID);
                sqlCommand.Parameters.AddWithValue("@PAGO_ID", boleto.PAGO_ID);
                sqlCommand.Parameters.AddWithValue("@BOL_FEC_IDA", boleto.BOL_FEC_IDA);
                sqlCommand.Parameters.AddWithValue("@BOL_FEC_VUELTA", boleto.BOL_FEC_VUELTA);
                sqlCommand.Parameters.AddWithValue("@BOL_PRECIO", boleto.BOL_PRECIO);
                sqlCommand.Parameters.AddWithValue("@BOL_ASIENTO", boleto.BOL_ASIENTO);
                sqlCommand.Parameters.AddWithValue("@BOL_TERMINAL", boleto.BOL_TERMINAL);
                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }

        [HttpPut]
        public IHttpActionResult Actualizar(Boleto boleto)
        {
            if (boleto == null)
                return BadRequest();

            if (ActualizarBoleto(boleto))
                return Ok(boleto);
            else
                return InternalServerError();
        }

        private bool ActualizarBoleto(Boleto boleto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE BOLETO SET USU_CODIGO = @USU_CODIGO
                                                                            ,VUE_ID = @VUE_ID
                                                                            ,PAGO_ID = @PAGO_ID
                                                                            ,BOL_FEC_IDA = @BOL_FEC_IDA
                                                                            ,BOL_FEC_VUELTA = @BOL_FEC_VUELTA
                                                                            ,BOL_PRECIO = @BOL_PRECIO
                                                                            ,BOL_ASIENTO = @BOL_ASIENTO
                                                                            ,BOL_TERMINAL = @BOL_TERMINAL
                                                                      WHERE BOL_ID = @BOL_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@BOL_ID", boleto.BOL_ID);
                sqlCommand.Parameters.AddWithValue("@USU_CODIGO", boleto.USU_CODIGO);
                sqlCommand.Parameters.AddWithValue("@VUE_ID", boleto.VUE_ID);
                sqlCommand.Parameters.AddWithValue("@PAGO_ID", boleto.PAGO_ID);
                sqlCommand.Parameters.AddWithValue("@BOL_FEC_IDA", boleto.BOL_FEC_IDA);
                sqlCommand.Parameters.AddWithValue("@BOL_FEC_VUELTA", boleto.BOL_FEC_VUELTA);
                sqlCommand.Parameters.AddWithValue("@BOL_PRECIO", boleto.BOL_PRECIO);
                sqlCommand.Parameters.AddWithValue("@BOL_ASIENTO", boleto.BOL_ASIENTO);
                sqlCommand.Parameters.AddWithValue("@BOL_TERMINAL", boleto.BOL_TERMINAL);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            if (EliminarBoleto(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarBoleto(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE BOLETO
                                                         WHERE BOL_ID = @BOL_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@BOL_ID", id);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }
    }
}