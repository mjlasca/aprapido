using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace ProyectoBrokerDelPuerto
{
    class ImportInstall
    {
        public static void ImportPropuestas(List<propuestas> listobj)
        {
            IDbConnection connection = ImportInstall.conn();
            using (var transaction = connection.BeginTransaction())
                                {
                                    try
                                    {
                                        // Crear comando con parámetros
                                        using (var insertCmd = connection.CreateCommand())
                                        {
                                            insertCmd.Transaction = transaction;
                                            insertCmd.CommandText = @"
                            INSERT INTO propuestas (
                                documento, num_polizas, meses, id_cobertura, id_barrio, nueva_poliza,
                                premio, premio_total, fechaDesde, fechaHasta, clausula, barrio_beneficiario,
                                ultmod, user_edit, codestado, cobertura_suma, cobertura_deducible,
                                cobertura_gastos, promocion, paga, fecha_paga, referencia, prima,
                                master, organizador, productor, prefijo, formadepago, usuariopaga,
                                tipopago, compformapago, idpropuesta, envionube, codempresa, nota,
                                data_barrios, version, valor_pagado, imputacion, fecha_comprobante
                            )
                            VALUES (
                                @documento, @num_polizas, @meses, @id_cobertura, @id_barrio, @nueva_poliza,
                                @premio, @premio_total, @fechaDesde, @fechaHasta, @clausula, @barrio_beneficiario,
                                @ultmod, @user_edit, @codestado, @cobertura_suma, @cobertura_deducible,
                                @cobertura_gastos, @promocion, @paga, @fecha_paga, @referencia, @prima,
                                @master, @organizador, @productor, @prefijo, @formadepago, @usuariopaga,
                                @tipopago, @compformapago, @idpropuesta, @envionube, @codempresa, @nota,
                                @data_barrios, @version, @valor_pagado, @imputacion, @fecha_comprobante
                            )";

                                            // Crear parámetros y agregarlos uno por uno
                                            var parameters = new[]
                                            {
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter()
                        };

                                            string[] paramNames = {
                            "@documento", "@num_polizas", "@meses", "@id_cobertura", "@id_barrio", "@nueva_poliza",
                            "@premio", "@premio_total", "@fechaDesde", "@fechaHasta", "@clausula", "@barrio_beneficiario",
                            "@ultmod", "@user_edit", "@codestado", "@cobertura_suma", "@cobertura_deducible", "@cobertura_gastos",
                            "@promocion", "@paga", "@fecha_paga", "@referencia", "@prima", "@master", "@organizador",
                            "@productor", "@prefijo", "@formadepago", "@usuariopaga", "@tipopago", "@compformapago", "@idpropuesta",
                            "@envionube", "@codempresa", "@nota", "@data_barrios", "@version", "@valor_pagado", "@imputacion", "@fecha_comprobante"
                        };

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i].ParameterName = paramNames[i];
                            insertCmd.Parameters.Add(parameters[i]);
                        }

                        // Insertar registros
                        foreach (var pros in listobj)
                        {
                            try
                            {
                                parameters[0].Value = pros.documento;
                                parameters[1].Value = pros.num_polizas;
                                parameters[2].Value = pros.meses;
                                parameters[3].Value = pros.id_cobertura;
                                parameters[4].Value = pros.id_barrio;
                                parameters[5].Value = pros.nueva_poliza;
                                parameters[6].Value = pros.premio;
                                parameters[7].Value = pros.premio_total;
                                parameters[8].Value = pros.fechaDesde;
                                parameters[9].Value = pros.fechaHasta;
                                parameters[10].Value = pros.clausula;
                                parameters[11].Value = pros.barrio_beneficiario;
                                parameters[12].Value = pros.ultmod;
                                parameters[13].Value = pros.user_edit;
                                parameters[14].Value = pros.codestado;
                                parameters[15].Value = pros.cobertura_suma;
                                parameters[16].Value = pros.cobertura_deducible;
                                parameters[17].Value = pros.cobertura_gastos;
                                parameters[18].Value = pros.promocion;
                                parameters[19].Value = pros.paga;
                                parameters[20].Value = pros.fecha_paga;
                                parameters[21].Value = pros.referencia;
                                parameters[22].Value = pros.prima;
                                parameters[23].Value = pros.master;
                                parameters[24].Value = pros.organizador;
                                parameters[25].Value = pros.productor;
                                parameters[26].Value = pros.prefijo;
                                parameters[27].Value = pros.formadepago;
                                parameters[28].Value = pros.usuariopaga;
                                parameters[29].Value = pros.tipopago;
                                parameters[30].Value = pros.compformapago;
                                parameters[31].Value = pros.idpropuesta;
                                parameters[32].Value = 1;
                                parameters[33].Value = MDIParent1.codempresa;
                                parameters[34].Value = pros.nota;
                                parameters[35].Value = pros.data_barrios;
                                parameters[36].Value = pros.version;
                                parameters[37].Value = pros.valor_pagado;
                                parameters[38].Value = pros.imputacion;
                                parameters[39].Value = pros.fecha_comprobante;

                                insertCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error en propuesta {pros.idpropuesta}: {ex.Message}");
                            }
                        }
                    }


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error general en ImportarDatos: {ex.Message}");
                }
            }
        }

        private static IDbConnection conn()
        {
            DbProviderFactory factory;
            if (MDIParent1.baseDatos == "MySql")
                factory = MySqlClientFactory.Instance;
            else if (MDIParent1.baseDatos == "SQlite")
                factory = SQLiteFactory.Instance;
            else
                throw new Exception("Base de datos no encontrada");
            IDbConnection connection = factory.CreateConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.ConnectionString = MDIParent1.strconn;
                connection.Open();
            }
            return connection;
        }

        public static void ImportLineasPropuestas(List<lineas_propuestas> listobj)
        {

            IDbConnection connection = ImportInstall.conn();
            // Crear transacción (opcional si quieres confirmar todo)
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Crear comando con parámetros
                    using (var insertCmd = connection.CreateCommand())
                    {
                        insertCmd.Transaction = transaction;
                        insertCmd.CommandText = @"
                            INSERT INTO lineas_propuestas (
                                id_propuesta,documento,tipo_documento,  apellidos, nombres,  fecha_nacimiento, id_actividad,
                                id_clasificacion, premio ,ultmod,user_edit,codestado,prefijo,actividad,clasificacion,fechaDesde,
                                fechaHasta, idprefijo,codempresa
                            )
                            VALUES (
                                @id_propuesta, @documento, @tipo_documento, @apellidos, @nombres, @fecha_nacimiento, @id_actividad,
                                @id_clasificacion, @premio, @ultmod, @user_edit, @codestado, @prefijo, @actividad, @clasificacion, @fechaDesde,
                                @fechaHasta, @idprefijo, @codempresa
                            )";

                        // Crear parámetros y agregarlos uno por uno 
                        var parameters = new[]
                        {
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter()
                        };

                        string[] paramNames = {
                            "@id_propuesta", "@documento", "@tipo_documento", "@apellidos", "@nombres", "@fecha_nacimiento",
                            "@id_actividad", "@id_clasificacion", "@premio", "@ultmod", "@user_edit", "@codestado",
                            "@prefijo", "@actividad", "@clasificacion", "@fechaDesde", "@fechaHasta", "@idprefijo", "@codempresa"
                        };


                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i].ParameterName = paramNames[i];
                            insertCmd.Parameters.Add(parameters[i]);
                        }

                        // Insertar registros
                        foreach (var lis in listobj)
                        {
                            try
                            {
                                parameters[0].Value = lis.id_propuesta;
                                parameters[1].Value = lis.documento;
                                parameters[2].Value = lis.tipo_documento;
                                parameters[3].Value = lis.apellidos;
                                parameters[4].Value = lis.nombres;
                                parameters[5].Value = lis.fecha_nacimiento;
                                parameters[6].Value = lis.id_actividad;
                                parameters[7].Value = lis.id_clasificacion;
                                parameters[8].Value = lis.premio;
                                parameters[9].Value = lis.ultmod;
                                parameters[10].Value = lis.user_edit;
                                parameters[11].Value = lis.codestado;
                                parameters[12].Value = lis.prefijo;
                                parameters[13].Value = lis.actividad;
                                parameters[14].Value = lis.clasificacion;
                                parameters[15].Value = lis.fechaDesde;
                                parameters[16].Value = lis.fechaHasta;
                                parameters[17].Value = lis.idprefijo;
                                parameters[18].Value = MDIParent1.codempresa;

                                insertCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error en propuesta {lis.prefijo} {lis.id_propuesta} : {ex.Message}");
                            }
                        }

                    }


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error general en ImportarDatos: {ex.Message}");
                }
            }
        }

        public static void ImportClientes(List<clientes> listobj)
        {

            IDbConnection connection = ImportInstall.conn();
            // Crear transacción (opcional si quieres confirmar todo)
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Crear comando con parámetros
                    using (var insertCmd = connection.CreateCommand())
                    {
                        insertCmd.Transaction = transaction;
                        insertCmd.CommandText = @"
                            INSERT INTO clientes (
                                id, nombres, apellidos, tipo_id, telefono, direccion, email, codpostal, 
                                localidad, ciudad, sexo, fecha_nacimiento, situacion, ultmod, user_edit,
                                codestado, categoria, codempresa, idaseguradora, envionube, cuir
                            )
                            VALUES (
                                @id, @nombres, @apellidos, @tipo_id, @telefono, @direccion, @email, @codpostal,
                                @localidad, @ciudad, @sexo, @fecha_nacimiento, @situacion, @ultmod, @user_edit,
                                @codestado, @categoria, @codempresa, @idaseguradora, @envionube, @cuir
                            )";

                                            // Crear parámetros y agregarlos
                        var parameters = new[]
                        {
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter()

                        };

                        string[] paramNames = {
                            "@id", "@nombres", "@apellidos", "@tipo_id", "@telefono", "@direccion", "@email", "@codpostal",
                            "@localidad", "@ciudad", "@sexo", "@fecha_nacimiento", "@situacion", "@ultmod", "@user_edit",
                            "@codestado", "@categoria", "@codempresa", "@idaseguradora", "@envionube", "@cuir"
                        };

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i].ParameterName = paramNames[i];
                            insertCmd.Parameters.Add(parameters[i]);
                        }

                        // Insertar registros
                        foreach (var cli in listobj)
                        {
                            try
                            {
                                parameters[0].Value = cli.id;
                                parameters[1].Value = cli.nombres;
                                parameters[2].Value = cli.apellidos;
                                parameters[3].Value = cli.tipo_id;
                                parameters[4].Value = cli.telefono;
                                parameters[5].Value = cli.direccion;
                                parameters[6].Value = cli.email;
                                parameters[7].Value = cli.codpostal;
                                parameters[8].Value = cli.localidad;
                                parameters[9].Value = cli.ciudad;
                                parameters[10].Value = cli.sexo;
                                parameters[11].Value = cli.fecha_nacimiento;
                                parameters[12].Value = cli.situacion;
                                parameters[13].Value = cli.ultmod;
                                parameters[14].Value = cli.user_edit;
                                parameters[15].Value = cli.codestado;
                                parameters[16].Value = cli.categoria;
                                parameters[17].Value = MDIParent1.codempresa; 
                                parameters[18].Value = cli.idaseguradora;
                                parameters[19].Value = 1;
                                parameters[20].Value = cli.cuir;

                                insertCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error en cliente {cli.id}: {ex.Message}");
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error general en ImportarDatos: {ex.Message}");
                }
            }
        }

        public static void ImportBarrios(List<barrios> listobj)
        {
            IDbConnection connection = ImportInstall.conn();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // 🧹 1. Eliminar todos los barrios primero
                    using (var deleteCmd = connection.CreateCommand())
                    {
                        deleteCmd.Transaction = transaction;
                        deleteCmd.CommandText = "DELETE FROM barrios";
                        deleteCmd.ExecuteNonQuery();
                    }

                    // 🧩 2. Insertar nuevos registros
                    using (var insertCmd = connection.CreateCommand())
                    {
                        insertCmd.Transaction = transaction;
                        insertCmd.CommandText = @"
                    INSERT INTO barrios (
                        id, nombre, telefono, direccion, email, sub_barrio, clase_barrio,
                        suma_muerte, suma_gm, suma_rc, exige, observaciones,
                        ultmod, user_edit, codestado, envionube
                    )
                    VALUES (
                        @id, @nombre, @telefono, @direccion, @email, @sub_barrio, @clase_barrio,
                        @suma_muerte, @suma_gm, @suma_rc, @exige, @observaciones,
                        @ultmod, @user_edit, @codestado, @envionube
                    )";

                        var parameters = new[]
                        {
                    insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                    insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                    insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                    insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter()
                };

                        string[] paramNames = {
                    "@id", "@nombre", "@telefono", "@direccion", "@email", "@sub_barrio", "@clase_barrio",
                    "@suma_muerte", "@suma_gm", "@suma_rc", "@exige", "@observaciones",
                    "@ultmod", "@user_edit", "@codestado", "@envionube"
                };

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i].ParameterName = paramNames[i];
                            insertCmd.Parameters.Add(parameters[i]);
                        }

                        foreach (var bar in listobj)
                        {
                            // ❌ Quitamos el try/catch interno
                            parameters[0].Value = bar.id;
                            parameters[1].Value = bar.nombre;
                            parameters[2].Value = bar.telefono;
                            parameters[3].Value = bar.direccion;
                            parameters[4].Value = bar.email;
                            parameters[5].Value = bar.sub_barrio;
                            parameters[6].Value = bar.clase_barrio;
                            parameters[7].Value = bar.suma_muerte;
                            parameters[8].Value = bar.suma_gm;
                            parameters[9].Value = bar.suma_rc;
                            parameters[10].Value = bar.exige;
                            parameters[11].Value = bar.observaciones;
                            parameters[12].Value = bar.ultmod;
                            parameters[13].Value = bar.user_edit;
                            parameters[14].Value = bar.codestado;
                            parameters[15].Value = 1;

                            insertCmd.ExecuteNonQuery(); // si falla → salta al catch general
                        }
                    }

                    // ✅ 3. Confirmar todo
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // 🔄 Revierte TODO (incluye DELETE)
                    transaction.Rollback();
                    Console.WriteLine($"Error general en ImportBarrios: {ex.Message}");
                }
            }
        }

        public static void ImportProvincias(List<provincias> listobj)
        {

            IDbConnection connection = ImportInstall.conn();
            // Crear transacción (opcional si quieres confirmar todo)
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Crear comando con parámetros
                    using (var insertCmd = connection.CreateCommand())
                    {
                        insertCmd.Transaction = transaction;
                        insertCmd.CommandText = @"
                                            INSERT INTO provincias (
                                                codpostal, provincia, ciudad, ultmod, user_edit, codestado
                                            )
                                            VALUES (
                                                @codpostal, @provincia, @ciudad, @ultmod, @user_edit, @codestado
                                            )";

                        // Crear parámetros y agregarlos
                        var parameters = new[]
                        {
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter()
                        };

                        string[] paramNames = {
                            "@codpostal", "@provincia", "@ciudad", "@ultmod", "@user_edit", "@codestado"
                        };

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i].ParameterName = paramNames[i];
                            insertCmd.Parameters.Add(parameters[i]);
                        }

                        // Insertar registros
                        foreach (var prov in listobj)
                        {
                            try
                            {
                                parameters[0].Value = prov.codpostal;
                                parameters[1].Value = prov.provincia;
                                parameters[2].Value = prov.ciudad;
                                parameters[3].Value = prov.ultmod;
                                parameters[4].Value = prov.user_edit;
                                parameters[5].Value = prov.codestado;

                                insertCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error en provincia {prov.codpostal}: {ex.Message}");
                            }
                        }
                    }



                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error general en ImportarDatos: {ex.Message}");
                }
            }
        }

        public static void ImportGruposBarrios(List<gruposbarrios> listobj)
        {

            IDbConnection connection = ImportInstall.conn();
            // Crear transacción (opcional si quieres confirmar todo)
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Crear comando con parámetros
                    using (var insertCmd = connection.CreateCommand())
                    {
                        insertCmd.Transaction = transaction;
                        insertCmd.CommandText = @"
                                    INSERT INTO gruposbarrios (
                                        id, nombre, idbarrio, nombrebarrio, ultmod, envionube, codestado
                                    )
                                    VALUES (
                                        @id, @nombre, @idbarrio, @nombrebarrio, @ultmod, @envionube, @codestado
                                    )";

                        // Crear parámetros y agregarlos
                        var parameters = new[]
                        {
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter(), insertCmd.CreateParameter(), insertCmd.CreateParameter(),
                            insertCmd.CreateParameter()
                        };

                        string[] paramNames = {
                            "@id", "@nombre", "@idbarrio", "@nombrebarrio", "@ultmod", "@envionube", "@codestado"
                        };

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i].ParameterName = paramNames[i];
                            insertCmd.Parameters.Add(parameters[i]);
                        }

                        // Insertar registros
                        foreach (var grup in listobj)
                        {
                            try
                            {
                                parameters[0].Value = grup.id;
                                parameters[1].Value = grup.nombre;
                                parameters[2].Value = grup.idbarrio;
                                parameters[3].Value = grup.nombrebarrio;
                                parameters[4].Value = grup.ultmod;
                                parameters[5].Value = 1; 
                                parameters[6].Value = grup.codestado;

                                insertCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error en grupo {grup.nombre} {grup.idbarrio}: {ex.Message}");
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error general en ImportarDatos: {ex.Message}");
                }
            }
        }

    }
}
