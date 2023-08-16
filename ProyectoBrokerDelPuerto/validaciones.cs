using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace ProyectoBrokerDelPuerto
{
    class validaciones
    {
        public string jsoncompare { set; get; } = "";
        public bool esnumero(string num)
        {

            try
            {
                double x = Convert.ToDouble(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public bool estavacio(string texto)
        {
            if (texto != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*
        * El formato es dd/mm/yyyy para KeyUp
        */
        public string fecha_textbox(string txt)
        {
            try
            {
                if (txt.Length == 2)
                {
                    if (Convert.ToUInt16(txt) > 0 && Convert.ToUInt16(txt) <= 31)
                    {
                        txt = txt + "/";
                    }
                }
                if (txt.Length == 5)
                {
                    txt = txt + "/";
                }
            }
            catch(Exception ex)
            {
                Console.Write("Error");
            }
            

            return txt;
        }

       




        public bool estexto(string stringToVerify)
        {
                stringToVerify = stringToVerify.Replace("'", "");
                stringToVerify = stringToVerify.Replace("á", "a");
                stringToVerify = stringToVerify.Replace("é", "e");
                stringToVerify = stringToVerify.Replace("í", "i");
                stringToVerify = stringToVerify.Replace("ó", "o");
                stringToVerify = stringToVerify.Replace("ú", "u");
                stringToVerify = stringToVerify.Replace("ñ", "n");
                stringToVerify = stringToVerify.Replace(" ", "");
                stringToVerify = stringToVerify.Replace(" ", "");
                stringToVerify = stringToVerify.Replace(" ", "");
                stringToVerify = stringToVerify.Replace(" ", "");

                for (int i = 0; i < stringToVerify.Length; i++)
                {
                    //A=65 Z=90 and a=97 z=122
                    if ((int)stringToVerify[i] < 65 || ((int)stringToVerify[i] > 90
                        && (int)stringToVerify[i] < 97) || (int)stringToVerify[i] > 122)
                        return false;
                }
                return true;
        }

        public bool correo(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool camposvacios_grupos_condescartados(GroupBox gr, string[] descartadosNom)
        {
            
            foreach (Control item in gr.Controls)
            {
                if (Array.IndexOf(descartadosNom, item.Name) < 0)
                {
                    if (item is TextBox)
                    {
                        item.Text = item.Text.Replace("'", "");
                        TextBox texto = item as TextBox;
                        if (texto.Text.Trim().Trim() == "")
                            return false;
                        
                    }
                }

            }

            return true;
        }

        public bool camposvacios_grupos(GroupBox gr)
        {
            foreach (Control item in gr.Controls)
            {
                if (item is TextBox)
                {
                    TextBox texto = item as TextBox;
                    if (texto.Text == "")
                        return false;
                }

            }

            return true;
        }

        // This method accepts two strings the represent two files to
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the
        // files are not the same.
        public bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is
            // equal to "file2byte" at this point only if the files are
            // the same.
            return ((file1byte - file2byte) == 0);
        }

        /**
         * Funciton date validate is succes full or not
         * 
         */
        public bool validateDate(string date)
        {
            DateTime fecha;
            if (DateTime.TryParse(date, out fecha))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool alguncampocondatos_grupos(GroupBox gr, string[] descartadosNom)
        {

            foreach (Control item in gr.Controls)
            {
                if (Array.IndexOf(descartadosNom, item.Name) < 0)
                {
                    if (item is TextBox)
                    {
                        TextBox texto = item as TextBox;
                        if (texto.Text != "")
                        {
                            return true;
                        }
                            
                    }
                }

            }

            return false;
        }

        public bool camposvacios_condescartados(Form frm, string[] descartadosNom)
        {
            foreach (Control item in frm.Controls)
            {
                if (Array.IndexOf(descartadosNom, item.Name) < 0)
                {
                    if (item is TextBox)
                    {
                        TextBox texto = item as TextBox;
                        if (texto.Text == "")
                            return false;

                    }
                }

            }

            return true;
        }

        public bool camposvacios(Form frm)
        {
            foreach (Control item in frm.Controls)
            {
                if (item is TextBox)
                {
                    TextBox texto = item as TextBox;
                    if (texto.Text == "")
                        return false;
                }

            }

            return true;
        }

        /*
         * Function, validate format string
         */
        public string fs_query(string text_)
        {
            text_ = text_.Replace("\n", "");
            text_ = text_.Replace("\n", "");
            text_ = text_.Replace("\r", "");
            text_ = text_.Replace("\r", "");
            text_ = text_.Replace("\r\n", "");
            text_ = text_.Replace("\r\n", "");
            text_ = text_.TrimEnd('\n');
            text_ = text_.Replace("\t", " ");
            text_ = text_.Replace("\t", " ");
            text_ = text_.Replace("\t", " ").Replace("\n", "");
            return text_.Trim();
        }

        public bool dataComparacion<T>(string solicitud, List<T> data)
        {
            if(data != null)
            {
                this.jsoncompare = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                if (!File.Exists(@"file_importaciones/importacion_" + solicitud + ".txt"))
                {
                    System.IO.Directory.CreateDirectory("file_importaciones");
                    System.IO.File.WriteAllText(@"file_importaciones/importacion_" + solicitud + ".txt", " ");
                }

                System.IO.File.WriteAllText(@"file_importaciones/importacion_" + solicitud + "1.txt", this.jsoncompare);

                if (this.FileCompare(@"file_importaciones/importacion_" + solicitud + ".txt", @"file_importaciones/importacion_" + solicitud + "1.txt"))
                {
                    return false;
                }

                return true;

            }

            return false;
        }

        public void createData0(string solicitud)
        {
            if(this.jsoncompare != null)
            {
                System.IO.File.WriteAllText(@"file_importaciones/importacion_" + solicitud + ".txt", this.jsoncompare);
            }
        }

    }
}
