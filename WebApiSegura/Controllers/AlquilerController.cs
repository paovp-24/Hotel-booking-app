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
    [RoutePrefix("api/alquiler")]
    public class AlquilerController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            Alquiler alquiler = new Alquiler();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ALQ_ID, USU_CODIGO, VEH_ID, PAGO_ID,
                                                             ALQ_FECHA_ENTREGA, ALQ_FECHA_ALQUILER, ALQ_PRECIOXHORA
                                                            FROM ALQUILER                                                           
                                                            WHERE ALQ_ID = @ALQ_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ALQ_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        alquiler.ALQ_ID = sqlDataReader.GetInt32(0);
                        alquiler.USU_CODIGO = sqlDataReader.GetInt32(1);
                        alquiler.VEH_ID = sqlDataReader.GetInt32(2);
                        alquiler.PAGO_ID = sqlDataReader.GetInt32(3);
                        alquiler.ALQ_FECHA_ENTREGA = sqlDataReader.GetDateTime(4);
                        alquiler.ALQ_FECHA_ALQUILER = sqlDataReader.GetDateTime(5);
                        alquiler.ALQ_PRECIOXHORA = sqlDataReader.GetDecimal(6);



                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(alquiler);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Alquiler> alquileres = new List<Alquiler>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT ALQ_ID, USU_CODIGO, VEH_ID, PAGO_ID,
                                                            ALQ_FECHA_ENTREGA, ALQ_FECHA_ALQUILER, ALQ_PRECIOXHORA
                                                            FROM ALQUILER", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Alquiler alquiler = new Alquiler()
                        {

                            ALQ_ID = sqlDataReader.GetInt32(0),
                            USU_CODIGO = sqlDataReader.GetInt32(1),
                            VEH_ID = sqlDataReader.GetInt32(2),
                            PAGO_ID = sqlDataReader.GetInt32(3),
                            ALQ_FECHA_ENTREGA = sqlDataReader.GetDateTime(4),
                            ALQ_FECHA_ALQUILER = sqlDataReader.GetDateTime(5),
                            ALQ_PRECIOXHORA = sqlDataReader.GetDecimal(6),
                        };

                        alquileres.Add(alquiler);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(alquileres);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Alquiler alquiler)
        {
            if (alquiler == null)
                return BadRequest();

            if (RegistrarAlquiler(alquiler))
                return Ok(alquiler);
            else
                return InternalServerError();
        }

        private bool RegistrarAlquiler(Alquiler alquiler)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO ALQUILER(USU_CODIGO, VEH_ID, PAGO_ID,
                                                            ALQ_FECHA_ENTREGA, ALQ_FECHA_ALQUILER, ALQ_PRECIOXHORA)
                                                            VALUES(@USU_CODIGO, @VEH_ID, @PAGO_ID,
                                                            @ALQ_FECHA_ENTREGA, @ALQ_FECHA_ALQUILER, @ALQ_PRECIOXHORA)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@ALQ_ID", alquiler.ALQ_ID);
                sqlCommand.Parameters.AddWithValue("@USU_CODIGO", alquiler.USU_CODIGO);
                sqlCommand.Parameters.AddWithValue("@VEH_ID", alquiler.VEH_ID);
                sqlCommand.Parameters.AddWithValue("@PAGO_ID", alquiler.PAGO_ID);
                sqlCommand.Parameters.AddWithValue("@ALQ_FECHA_ENTREGA", alquiler.ALQ_FECHA_ENTREGA);
                sqlCommand.Parameters.AddWithValue("@ALQ_FECHA_ALQUILER", alquiler.ALQ_FECHA_ALQUILER);
                sqlCommand.Parameters.AddWithValue("@ALQ_PRECIOXHORA", alquiler.ALQ_PRECIOXHORA);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }

        [HttpPut]
        public IHttpActionResult Actualizar(Alquiler alquiler)
        {
            if (alquiler == null)
                return BadRequest();

            if (ActualizarAlquiler(alquiler))
                return Ok(alquiler);
            else
                return InternalServerError();
        }

        private bool ActualizarAlquiler(Alquiler alquiler)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE ALQUILER SET USU_CODIGO = @USU_CODIGO,
                                                                             VEH_ID = @VEH_ID,
                                                                             PAGO_ID = @PAGO_ID,
                                                                             ALQ_FECHA_ENTREGA = @ALQ_FECHA_ENTREGA,
                                                                             ALQ_FECHA_ALQUILER = @ALQ_FECHA_ALQUILER,
                                                                             ALQ_PRECIOXHORA = @ALQ_PRECIOXHORA                                                                         
                                                                             WHERE ALQ_ID = @ALQ_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@ALQ_ID", alquiler.ALQ_ID);
                sqlCommand.Parameters.AddWithValue("@USU_CODIGO", alquiler.USU_CODIGO);
                sqlCommand.Parameters.AddWithValue("@VEH_ID", alquiler.VEH_ID);
                sqlCommand.Parameters.AddWithValue("@PAGO_ID", alquiler.PAGO_ID);
                sqlCommand.Parameters.AddWithValue("@ALQ_FECHA_ENTREGA", alquiler.ALQ_FECHA_ENTREGA);
                sqlCommand.Parameters.AddWithValue("@ALQ_FECHA_ALQUILER", alquiler.ALQ_FECHA_ALQUILER);
                sqlCommand.Parameters.AddWithValue("@ALQ_PRECIOXHORA", alquiler.ALQ_PRECIOXHORA);

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

            if (EliminarAlquiler(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarAlquiler(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE ALQUILER
                                                         WHERE ALQ_ID = @ALQ_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@ALQ_ID", id);

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


