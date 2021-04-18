using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;
namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/pago")]
    public class PagoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Pago pago = new Pago();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PAGO_ID , PAGO_FECHA, PAGO_TOTAL , PAGO_ESTADO, PAGO_DESCRIPCION FROM PAGO
                                                            WHERE PAGO_ID = @PAGO_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAGO_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        pago.PAGO_ID = sqlDataReader.GetInt32(0);
                        pago.PAGO_FECHA = sqlDataReader.GetDateTime(1);
                        pago.PAGO_TOTAL = sqlDataReader.GetDecimal(2);
                        pago.PAGO_ESTADO = sqlDataReader.GetString(3);
                        pago.PAGO_DESCRIPCION = sqlDataReader.GetString(4);


                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(pago);
        }

        [HttpGet]

        public IHttpActionResult GetAll()
        {
            List<Pago> pagos = new List<Pago>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PAGO_ID, PAGO_FECHA, PAGO_TOTAL, PAGO_ESTADO, PAGO_DESCRIPCION
                                                             FROM PAGO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Pago pago = new Pago()
                        {
                            PAGO_ID = sqlDataReader.GetInt32(0),
                            PAGO_FECHA = sqlDataReader.GetDateTime(1),
                            PAGO_TOTAL = sqlDataReader.GetDecimal(2),
                            PAGO_ESTADO = sqlDataReader.GetString(3),
                            PAGO_DESCRIPCION = sqlDataReader.GetString(4),

                        };

                        pagos.Add(pago);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(pagos);
        }
        [HttpPost]
        public IHttpActionResult Ingresar(Pago pago)
        {
            if (pago == null)
                return BadRequest();

            if (RegistrarPago(pago))
                return Ok(pago);
            else
                return InternalServerError();
        }

        private bool RegistrarPago(Pago pago)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT  INTO PAGO ( PAGO_FECHA, PAGO_TOTAL , PAGO_ESTADO, PAGO_DESCRIPCION) VALUES (
                                                                           @PAGO_FECHA, @PAGO_TOTAL , @PAGO_ESTADO, @PAGO_DESCRIPCION)", sqlConnection);


                sqlCommand.Parameters.AddWithValue("@PAGO_FECHA", pago.PAGO_FECHA);
                sqlCommand.Parameters.AddWithValue("@PAGO_TOTAL", pago.PAGO_TOTAL);
                sqlCommand.Parameters.AddWithValue("@PAGO_ESTADO", pago.PAGO_ESTADO);
                sqlCommand.Parameters.AddWithValue("@PAGO_DESCRIPCION", pago.PAGO_DESCRIPCION);


                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }

        [HttpPut]
        public IHttpActionResult Actualizar(Pago pago)
        {
            if (pago == null)
                return BadRequest();

            if (ActualizarPago(pago))
                return Ok(pago);
            else
                return InternalServerError();
        }

        private bool ActualizarPago(Pago pago)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE PAGO SET 
                                                                          PAGO_FECHA = @PAGO_FECHA, 
                                                                          PAGO_TOTAL= @PAGO_TOTAL, 
                                                                          PAGO_ESTADO = @PAGO_ESTADO, 
                                                                          PAGO_DESCRIPCION = @PAGO_DESCRIPCION
                                                                      WHERE PAGO_ID = @PAGO_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@PAGO_ID", pago.PAGO_ID);
                sqlCommand.Parameters.AddWithValue("@PAGO_FECHA", pago.PAGO_FECHA);
                sqlCommand.Parameters.AddWithValue("@PAGO_TOTAL", pago.PAGO_TOTAL);
                sqlCommand.Parameters.AddWithValue("@PAGO_ESTADO", pago.PAGO_ESTADO);
                sqlCommand.Parameters.AddWithValue("@PAGO_DESCRIPCION", pago.PAGO_DESCRIPCION);

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
            if (EliminarPago(id))
                return Ok(id);
            else
                return InternalServerError();
        }
        private bool EliminarPago(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" DELETE PAGO  WHERE PAGO_ID= @PAGO_ID  ", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@PAGO_ID", id);


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

