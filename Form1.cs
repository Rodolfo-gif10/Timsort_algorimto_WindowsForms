using System;
using System.Windows.Forms;

namespace Timsort_EstructuraII_2    
{
    public partial class Form1 : Form
    {
        private const int RUN = 32;

        public Form1()
        {
            InitializeComponent();
            // Configuración del DataGridView
            dataGridViewNombres.ColumnCount = 1;
            dataGridViewNombres.Columns[0].Name = "Nombres";
            dataGridViewNombres.AllowUserToAddRows = false;
        }

        private void InsertionSort(string[] arreglo, int izquierda, int derecha)
        {
            for (int i = izquierda + 1; i <= derecha; i++)
            {
                string clave = arreglo[i];
                int j = i - 1;

                while (j >= izquierda && arreglo[j].CompareTo(clave) > 0)
                {
                    arreglo[j + 1] = arreglo[j];
                    j--;
                }
                arreglo[j + 1] = clave;
            }
        }

        private void Merge(string[] arreglo, int izquierda, int medio, int derecha)
        {       //se suma 1 para incluir el elemento medio
            int longitud1 = medio - izquierda + 1;
            int longitud2 = derecha - medio;
            string[] arregloIzquierda = new string[longitud1];
            string[] arregloDerecha = new string[longitud2];

            Array.Copy(arreglo, izquierda, arregloIzquierda, 0, longitud1);
            Array.Copy(arreglo, medio + 1, arregloDerecha, 0, longitud2);

            int i = 0, j = 0, k = izquierda;

            while (i < longitud1 && j < longitud2)
            {
               
                if (arregloIzquierda[i].CompareTo(arregloDerecha[j]) <= 0)
                {
                    arreglo[k++] = arregloIzquierda[i++];
                }
                else
                {
                    arreglo[k++] = arregloDerecha[j++];
                }
            }
            // ciclo para copiar cualquier elemento restante en
            // arregloIzquierda al arreglo original
            while (i < longitud1)
            {
                //Agrega los elementos restantes de arregloIzquierda.
                arreglo[k++] = arregloIzquierda[i++];
            }

            while (j < longitud2)
            {
                arreglo[k++] = arregloDerecha[j++];
            }
        }

        private void TimSort(string[] arreglo)
        {   // n captura la cantidad de elementos del arreglo
            int n = arreglo.Length;  

            // Realiza InsertionSort en bloques de tamaño RUN
            for (int i = 0; i < n; i += RUN)
            {                              
                InsertionSort(arreglo, i, Math.Min(i + RUN - 1, n - 1));
            }

            // Mezcla los bloques               
            for (int tamaño = RUN; tamaño < n; tamaño *= 2)
            {
                for (int izquierda = 0; izquierda < n; izquierda += 2 * tamaño)
                {
                    int medio = izquierda + tamaño - 1;
                    int derecha = Math.Min((izquierda + 2 * tamaño - 1), (n - 1));

                    if (medio < derecha)
                    {
                        Merge(arreglo, izquierda, medio, derecha);
                    }
                }
            }
        }


        private void btnOrdenar_Click_1(object sender, EventArgs e)
        {
            string[] nombres = txtNombres.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            TimSort(nombres);

            // Limpiar el DataGridView
            dataGridViewNombres.Rows.Clear();

            // Agregar los nombres ordenados al DataGridView
            foreach (var nombre in nombres)
            {
                dataGridViewNombres.Rows.Add(nombre);
            }
        }
    }
}
