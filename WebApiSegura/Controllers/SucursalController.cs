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
    [RoutePrefix("api/sucursal")]
    public class SucursalController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Sucursal sucursal = new Sucursal();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT SUC_ID , SUC_NOMBRE, SUB_UBICACION , SUC_CORREO, SUC_TELEFONO FROM SUCURSAL
                                                            WHERE SUC_ID = @SUC_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SUC_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        sucursal.SUC_ID = sqlDataReader.GetInt32(0);
                        sucursal.SUC_NOMBRE = sqlDataReader.GetString(1);
                        sucursal.SUB_UBICACION = sqlDataReader.GetString(2);
                        sucursal.SUC_CORREO = sqlDataReader.GetString(3);
                        sucursal.SUC_TELEFONO = sqlDataReader.GetInt32(4);


                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(sucursal);
        }

        [HttpGet]

        public IHttpActionResult GetAll()
        {
            List<Sucursal> sucursales = new List<Sucursal>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT SUC_ID , SUC_NOMBRE, SUB_UBICACION , SUC_CORREO, SUC_TELEFONO FROM SUCURSAL", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Sucursal sucursal = new Sucursal();
                        {
                            sucursal.SUC_ID = sqlDataReader.GetInt32(0);
                            sucursal.SUC_NOMBRE = sqlDataReader.GetString(1);
                            sucursal.SUB_UBICACION = sqlDataReader.GetString(2);
                            sucursal.SUC_CORREO = sqlDataReader.GetString(3);
                            sucursal.SUC_TELEFONO = sqlDataReader.GetInt32(4);

                        };

                        sucursales.Add(sucursal);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(sucursales);
        }
        [HttpPost]
        public IHttpActionResult Ingresar(Sucursal sucursal)
        {
            if (sucursal == null)
                return BadRequest();

            if (RegistrarSucursal(sucursal))
                return Ok(sucursal);
            else
                return InternalServerError();
        }

        private bool RegistrarSucursal(Sucursal sucursal)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT  INTO SUCURSAL ( SUC_NOMBRE, SUB_UBICACION , SUC_CORREO, SUC_TELEFONO ) VALUES (
                                                                           @SUC_NOMBRE, @SUC_UBICACION , @SUC_CORREO, @SUC_TELEFONO )", sqlConnection);


                sqlCommand.Parameters.AddWithValue("@SUC_NOMBRE", sucursal.SUC_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@SUC_UBICACION", sucursal.SUB_UBICACION);
                sqlCommand.Parameters.AddWithValue("@SUC_CORREO", sucursal.SUC_CORREO);
                sqlCommand.Parameters.AddWithValue("@SUC_TELEFONO", sucursal.SUC_TELEFONO);


                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }

        [HttpPut]
        public IHttpActionResult Actualizar(Sucursal sucursal)
        {
            if (sucursal == null)
                return BadRequest();

            if (ActualizarSucursal(sucursal))
                return Ok(sucursal);
            else
                return InternalServerError();
        }

        private bool ActualizarSucursal(Sucursal sucursal)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE Sucursal SET 
                                                                          SUC_NOMBRE= @SUC_NOMBRE, 
                                                                          SUB_UBICACION= @SUB_UBICACION, 
                                                                          SUC_CORREO = @SUC_CORREO, 
                                                                          SUC_TELEFONO = @SUC_TELEFONO
                                                                      WHERE SUC_ID = @SUC_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@SUC_ID", sucursal.SUC_ID);
                sqlCommand.Parameters.AddWithValue("@SUC_NOMBRE", sucursal.SUC_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@SUB_UBICACION", sucursal.SUB_UBICACION);
                sqlCommand.Parameters.AddWithValue("@SUC_CORREO", sucursal.SUC_CORREO);
                sqlCommand.Parameters.AddWithValue("@SUC_TELEFONO", sucursal.SUC_TELEFONO);

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
            if (EliminarSucursal(id))
                return Ok(id);
            else
                return InternalServerError();
        }
        private bool EliminarSucursal(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" DELETE SUCURSAL  WHERE SUC_ID= @SUC_ID  ", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@SUC_ID", id);


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
