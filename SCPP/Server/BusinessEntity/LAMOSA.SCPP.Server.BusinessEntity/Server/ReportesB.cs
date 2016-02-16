using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
    public class ReportesB
    {
        public DataTable ListDefectos(int planta, int proceso, int tipo_articulo, int modelo, int color, DateTime fechaIni, DateTime fechaFin, int vaciador, int estado, int iCentroTrabajo)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spReporteAdministracionDefecto";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (planta > 0) command.Parameters.AddWithValue("@CodigoPlanta", planta);
                if (proceso > 0) command.Parameters.AddWithValue("@CodigoProceso", proceso);
                if (tipo_articulo > 0) command.Parameters.AddWithValue("@CodigoTipoArticulo", tipo_articulo);
                if (modelo > 0) command.Parameters.AddWithValue("@CodigoArticulo", modelo);
                if (color > 0) command.Parameters.AddWithValue("@CodigoColor", color);
                if (vaciador > 0) command.Parameters.AddWithValue("@vaciador", vaciador);
                if (estado > 0) command.Parameters.AddWithValue("@estado", estado);
                if (iCentroTrabajo > 0) command.Parameters.AddWithValue("@CodigoCentroTrabajo", iCentroTrabajo);
                command.Parameters.AddWithValue("@FechaIni", fechaIni);
                command.Parameters.AddWithValue("@FechaFin", fechaFin);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();

                DataSet dsResult = new DataSet();
                while (!dr.IsClosed)
                {
                    DataTable dtContenedor = new DataTable();
                    dtContenedor.Clear();
                    dtContenedor.Load(dr);
                    dsResult.Tables.Add(dtContenedor);
                }
                if (dsResult.Tables.Count < 2) return dt;
                dt = dsResult.Tables[0];
                if (dt.Rows.Count < 1) return dt;
                DataRow drTotal = dt.NewRow();
                int columnIni = (dt.Columns.Count - (dsResult.Tables[1].Columns.Count * 2)) - 1;
                drTotal[columnIni] = "Total:";
                for (int i = 0; i < dsResult.Tables[1].Columns.Count; i++)
                {
                    columnIni += 2;
                    drTotal[columnIni] = dsResult.Tables[1].Rows[0][i];
                }
                dt.Rows.Add(drTotal);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ListDefectosExcel(int planta, int proceso, int tipo_articulo, int modelo, int color, int iCodEstadoDefecto, int iCentroTrabajo, DateTime dtFechaIni, DateTime dtFechaFin)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spReporteAdministracionDefectoExportar";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (planta > 0) command.Parameters.AddWithValue("@CodigoPlanta", planta);
                if (proceso > 0) command.Parameters.AddWithValue("@CodigoProceso", proceso);
                if (tipo_articulo > 0) command.Parameters.AddWithValue("@CodigoTipoArticulo", tipo_articulo);
                if (modelo > 0) command.Parameters.AddWithValue("@CodigoArticulo", modelo);
                if (color > 0) command.Parameters.AddWithValue("@CodigoColor", color);
                if (iCodEstadoDefecto > 0) command.Parameters.AddWithValue("@CodigoEstadoDefecto", iCodEstadoDefecto);
                if (iCentroTrabajo > 0) command.Parameters.AddWithValue("@CodigoCentroTrabajo", iCentroTrabajo);
                command.Parameters.AddWithValue("@FechaIni", dtFechaIni);
                command.Parameters.AddWithValue("@FechaFin", dtFechaFin);

                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();

                dt.Load(dr);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ListDefectosDetalles(int planta, int proceso, int tipo_articulo, int modelo, int color, DateTime fechaIni, DateTime fechaFin, int vaciador, int estado, int defecto, int zona)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Inventario_Defecto3";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_planta", planta);
                command.Parameters.AddWithValue("@cod_proceso", proceso);
                command.Parameters.AddWithValue("@cod_tipo_articulo", tipo_articulo);
                command.Parameters.AddWithValue("@cod_modelo", modelo);
                command.Parameters.AddWithValue("@cod_color", color);
                command.Parameters.AddWithValue("@fecha_ini", fechaIni);
                command.Parameters.AddWithValue("@fecha_fin", fechaFin);
                command.Parameters.AddWithValue("@vaciador", vaciador);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@cod_defecto", defecto);
                command.Parameters.AddWithValue("@cod_zona", zona);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Pisos(int planta, int tipo_articulo, int modelo, int iCentroTrabajo, DateTime dtFechaIni, DateTime dtFechaFin)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spReporteAdministracionPlanta";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (planta > 0) command.Parameters.AddWithValue("@CodigoPlanta", planta);
                if (tipo_articulo > 0) command.Parameters.AddWithValue("@CodigoTipoArticulo", tipo_articulo);
                if (modelo > 0) command.Parameters.AddWithValue("@CodigoArticulo", modelo);
                if (iCentroTrabajo > 0) command.Parameters.AddWithValue("@CodigoCentroTrabajo", iCentroTrabajo);
                command.Parameters.AddWithValue("@FechaIni", dtFechaIni);
                command.Parameters.AddWithValue("@FechaFin", dtFechaFin);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();

                DataSet dsResult = new DataSet();
                while (!dr.IsClosed)
                {
                    DataTable dtContenedor = new DataTable();
                    dtContenedor.Clear();
                    dtContenedor.Load(dr);
                    dsResult.Tables.Add(dtContenedor);
                }
                if (dsResult.Tables.Count < 2) return dt;
                dt = dsResult.Tables[0];
                if (dt.Rows.Count < 1) return dt;
                DataRow drTotal = dt.NewRow();
                int columnIni = (dt.Columns.Count - dsResult.Tables[1].Columns.Count) - 1;
                drTotal[columnIni++] = "Total:";
                for (int i = 0; i < dsResult.Tables[1].Columns.Count; i++)
                {
                    drTotal[columnIni++] = dsResult.Tables[1].Rows[0][i];
                }
                dt.Rows.Add(drTotal);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ControlDePisos(int iCodigoPlanta, int iCodigoTurno, int iCodigoProcesoOrigen, int iCodigoProcesoDestino, int iCodigoTipoArticulo, int iCodigoModelo, int iCodigoCentroTrabajo, DateTime dtFechaInicial, DateTime dtFechaFinal)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandTimeout = 180;
                command.CommandText = "spReporteAdministracionProduccionPiso";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (iCodigoPlanta > 0) command.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                if (iCodigoTurno > 0) command.Parameters.AddWithValue("@CodigoTurno", iCodigoTurno);
                command.Parameters.AddWithValue("@CodigoProcesoOrigen", iCodigoProcesoOrigen);
                command.Parameters.AddWithValue("@CodigoProcesoDestino", iCodigoProcesoDestino);
                if (iCodigoTipoArticulo > 0) command.Parameters.AddWithValue("@CodigoTipoArticulo", iCodigoTipoArticulo);
                if (iCodigoModelo > 0) command.Parameters.AddWithValue("@CodigoArticulo", iCodigoModelo);
                if (iCodigoCentroTrabajo > 0) command.Parameters.AddWithValue("@CodigoCentroTrabajo", iCodigoCentroTrabajo);
                command.Parameters.AddWithValue("@FechaIni", dtFechaInicial);
                command.Parameters.AddWithValue("@FechaFin", dtFechaFinal);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();

                DataSet dsResult = new DataSet();
                while (!dr.IsClosed)
                {
                    DataTable dtContenedor = new DataTable();
                    dtContenedor.Clear();
                    dtContenedor.Load(dr);
                    dsResult.Tables.Add(dtContenedor);
                }
                if (dsResult.Tables.Count < 2) return dt;
                dt = dsResult.Tables[0];
                if (dt.Rows.Count < 1) return dt;
                dt.Columns[0].AllowDBNull = true;
                dt.Columns[2].AllowDBNull = true;
                DataRow drTotal = dt.NewRow();
                //drTotal[4] = Convert.ToString(((Convert.ToInt32(drTotal[3]) * 100) / Convert.ToInt32(drTotal[2])));
                //dsResult.Tables[1].Rows[0][2] = Convert.ToInt32(((Convert.ToInt32(dsResult.Tables[1].Rows[0][1]) * 100) / Convert.ToInt32(dsResult.Tables[1].Rows[0][0])));
                //drTotal[7] = Convert.ToString(((Convert.ToInt32(drTotal[6]) * 100) / Convert.ToInt32(drTotal[5])));
                //dsResult.Tables[1].Rows[0][5] = Convert.ToInt32(((Convert.ToInt32(dsResult.Tables[1].Rows[0][4]) * 100) / Convert.ToInt32(dsResult.Tables[1].Rows[0][3])));
                //drTotal[8] = Convert.ToInt32(((Convert.ToInt32(drTotal[3]) + Convert.ToInt32(drTotal[6])) * 100) / Convert.ToInt32(drTotal[5]));
                //dsResult.Tables[1].Rows[0][6] = Convert.ToInt32(((Convert.ToInt32(dsResult.Tables[1].Rows[0][1]) + Convert.ToInt32(dsResult.Tables[1].Rows[0][4])) * 100) / Convert.ToInt32(dsResult.Tables[1].Rows[0][0]));
                int columnIni = (dt.Columns.Count - dsResult.Tables[1].Columns.Count) - 1;
                drTotal[columnIni++] = "Total:";
                for (int i = 0; i < dsResult.Tables[1].Columns.Count; i++)
                {
                    drTotal[columnIni++] = dsResult.Tables[1].Rows[0][i];
                }
                dt.Rows.Add(drTotal);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
    }
}
