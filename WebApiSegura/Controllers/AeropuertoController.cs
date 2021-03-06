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
    [RoutePrefix("api/aeropuerto")]
    public class AeropuertoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Aeropuerto aeropuerto = new Aeropuerto();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AERO_ID, AERO_NOMBRE, AERO_PAIS, AERO_CIUDAD, AERO_TIPO
                                                            FROM     AEROPUERTO
                                                            WHERE AERO_ID = @AERO_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AERO_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        aeropuerto.AERO_ID = sqlDataReader.GetInt32(0);
                        aeropuerto.AERO_NOMBRE = sqlDataReader.GetString(1);
                        aeropuerto.AERO_PAIS = sqlDataReader.GetString(2);
                        aeropuerto.AERO_CIUDAD = sqlDataReader.GetString(3);
                        aeropuerto.AERO_TIPO = sqlDataReader.GetString(4);



                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(aeropuerto);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Aeropuerto> aeropuertos = new List<Aeropuerto>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AERO_ID, AERO_NOMBRE, AERO_PAIS, AERO_CIUDAD, AERO_TIPO
                                                            FROM     AEROPUERTO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Aeropuerto aeropuerto = new Aeropuerto()
                        {
                            AERO_ID = sqlDataReader.GetInt32(0),
                            AERO_NOMBRE = sqlDataReader.GetString(1),
                            AERO_PAIS = sqlDataReader.GetString(2),
                            AERO_CIUDAD = sqlDataReader.GetString(3),
                            AERO_TIPO = sqlDataReader.GetString(4),

                        };

                        aeropuertos.Add(aeropuerto);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(aeropuertos);
        }



        [HttpPost]
        public IHttpActionResult Ingresar(Aeropuerto aeropuerto)
        {
            if (aeropuerto == null)
                return BadRequest();

            if (RegistrarAero(aeropuerto))
                return Ok(aeropuerto);
            else
                return InternalServerError();
        }

        private bool RegistrarAero(Aeropuerto aeropuerto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO AEROPUERTO (AERO_NOMBRE, AERO_PAIS, 
                                                        AERO_CIUDAD, AERO_TIPO) VALUES (@AERO_NOMBRE,
                                                        @AERO_PAIS, @AERO_CIUDAD, @AERO_TIPO)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@AERO_NOMBRE", aeropuerto.AERO_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@AERO_PAIS", aeropuerto.AERO_PAIS);
                sqlCommand.Parameters.AddWithValue("@AERO_CIUDAD", aeropuerto.AERO_CIUDAD);
                sqlCommand.Parameters.AddWithValue("@AERO_TIPO", aeropuerto.AERO_TIPO);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }




        [HttpPut]
        public IHttpActionResult Actualizar(Aeropuerto aeropuerto)
        {
            if (aeropuerto == null)
                return BadRequest();

            if (ActualizarAero(aeropuerto))
                return Ok(aeropuerto);
            else
                return InternalServerError();
        }

        private bool ActualizarAero(Aeropuerto aeropuerto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE AEROPUERTO SET  
                                                                          AERO_NOMBRE = @AERO_NOMBRE, 
                                                                          AERO_PAIS = @AERO_PAIS, 
                                                                          AERO_CIUDAD = @AERO_CIUDAD, 
                                                                          AERO_TIPO = @AERO_TIPO
                                                                      WHERE AERO_ID = @AERO_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@AERO_ID", aeropuerto.AERO_ID);
                sqlCommand.Parameters.AddWithValue("@AERO_NOMBRE", aeropuerto.AERO_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@AERO_PAIS", aeropuerto.AERO_PAIS);
                sqlCommand.Parameters.AddWithValue("@AERO_CIUDAD", aeropuerto.AERO_CIUDAD);
                sqlCommand.Parameters.AddWithValue("@AERO_TIPO", aeropuerto.AERO_TIPO);

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

            if (EliminarAerol(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarAerol(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE AEROPUERTO
                                                         WHERE AERO_ID = @AERO_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@AERO_ID", id);

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