using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Dominio
{
    public class RegistroExcelAlianza : RegistroExcel
    {
        public class EstructuraExcelAlianzas
        {
            public const int COL_TITULO = 0;
            public const int COL_BENEFICIOS = 1;
            public const int COL_CATEGORIAS = 2;
            public const int COL_CATEGORIAPRINCIPAL = 3;
            public const int COL_TIENESUCURSALES = 4;
            public const int COL_DIRECCION = 5;
            public const int COL_LATITUD = 6;
            public const int COL_LONGITUD = 7;
            public const int COL_HIGHLIGHT = 8;
            public const int COL_LOGO = 9;
            public const int COL_SITIOWEB = 10;
            public const int COL_ORDEN = 11;
            public const int COL_ACTIVO = 12;
            public const int COL_ID = 13;
            public const int COL_IDIMPORTACION = 14;
        }

        public int CodAlianza { get; set; }
        public string Titulo { get; set; }
        public string Beneficios { get; set; }
        public string Categorias { get; set; }
        public string CategoriaPrincipal { get; set; }
        public string TieneSucursales { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Highlight { get; set; }
        public string Logo { get; set; }
        public string SitioWeb { get; set; }
        public string Orden { get; set; }
        public string Activo { get; set; }
        public string ID { get; set; }
        //public int IdImportacion { get; set; }

        private bool tituloValido;
        private bool beneficioValido;
        private bool categoriaValida;
        private bool categoriaPrincipalValida;
        private bool ordenValido;
        private bool activoValido;

        private string beneficioInexistente;
        private string categoriaInexistente;
        private string categoriaPrincipalInexistente;

        // Para validación y manejo de errores
        public override bool RegistroValido
        {
            get
            {
                tituloValido = !string.IsNullOrEmpty(Titulo);
                beneficioValido = !string.IsNullOrEmpty(Beneficios) && EsBeneficioValido;
                categoriaValida = !string.IsNullOrEmpty(Categorias) && EsCategoriaValida;
                categoriaPrincipalValida = string.IsNullOrEmpty(CategoriaPrincipal) || (!string.IsNullOrEmpty(CategoriaPrincipal) && EsCategoriaPrincipalValida);
                ordenValido = string.IsNullOrEmpty(Orden) || (!string.IsNullOrEmpty(Orden) && Regex.IsMatch(Orden, @"^\d+$"));
                activoValido = !string.IsNullOrEmpty(Activo) && (Activo == "1" || Activo == "0");

                return tituloValido && beneficioValido && categoriaValida && categoriaPrincipalValida && ordenValido && activoValido;
            }
        }


        public override string DetalleError
        {
            get
            {
                string errores = string.Empty;

                if (!tituloValido)
                    errores += "El título de la alianza es obligatorio.";

                if (!beneficioValido)
                {
                    if (string.IsNullOrEmpty(beneficioInexistente))
                        errores += "Se debe ingresar una lista de códigos de beneficios válidos.";
                    else
                        errores += $"Estos beneficios no existen: {beneficioInexistente}.";
                }

                if (!categoriaValida)
                {
                    if (string.IsNullOrEmpty(categoriaInexistente))
                        errores += "Se debe ingresar una lista de categorías válidas.";
                    else
                        errores += $"Estas categorías no existen: {categoriaInexistente}.";
                }

                if (!categoriaPrincipalValida)
                {
                    if (string.IsNullOrEmpty(categoriaPrincipalInexistente))
                        errores += "En la categoría principal, se debe ingresar una categoría válida.";
                    else
                        errores += $"Esta categoría principal no existe: {categoriaPrincipalInexistente}.";
                }

                if (!ordenValido)
                    errores += "Se debe ingresar un número de orden válido.";

                if (!activoValido)
                    errores += "El valor de dato \"activo\" debe ser 0 o 1.";

                return errores;
            }
        }

        public bool EsBeneficioValido
        {
            get
            {
                string[] beneficios = Beneficios.Split(",".ToCharArray()).Select(s => s.Trim()).ToArray();
                beneficioInexistente = string.Empty;

                foreach (string beneficio in beneficios)
                {
                    if (!Regex.IsMatch(beneficio, @"^\d+$"))
                        return false;
                }

                List<Beneficio> beneficiosExistentes = CacheHelper.GetOrAddToCache("BeneficiosExistentes", () => CacheHelper.Beneficios(), TimeSpan.FromMinutes(30));
                foreach (string beneficio in beneficios)
                {
                    if (beneficiosExistentes.FirstOrDefault(b => b.CodBeneficio == int.Parse(beneficio)) == null)
                    {
                        beneficioInexistente += $"{beneficio}, ";
                    }
                }
                return string.IsNullOrEmpty(beneficioInexistente);
            }
        }

        public bool EsCategoriaValida
        {
            get
            {
                string[] categorias = Categorias.Split(",".ToCharArray()).Select(s => s.Trim()).ToArray();
                List<Categoria> categoriasParam = CacheHelper.GetOrAddToCache("Categorias", () => CacheHelper.Categorias(), TimeSpan.FromMinutes(30));
                categoriaInexistente = string.Empty;

                foreach (string categoria in categorias)
                {
                    if (categoriasParam.FirstOrDefault(m => m.Slug == categoria.Trim().ToLower()) == null)
                    {
                        categoriaInexistente += $"{categoria}, ";
                    }
                }
                return string.IsNullOrEmpty(categoriaInexistente);
            }
        }

        public bool EsCategoriaPrincipalValida
        {
            get
            {
                string[] categorias = CategoriaPrincipal.Split(",".ToCharArray()).Select(s => s.Trim()).ToArray();
                List<Categoria> categoriasParam = CacheHelper.GetOrAddToCache("Categorias", () => CacheHelper.Categorias(), TimeSpan.FromMinutes(30));
                categoriaPrincipalInexistente = string.Empty;

                foreach (string categoria in categorias)
                {
                    if (categoriasParam.FirstOrDefault(m => m.Slug == categoria.Trim().ToLower()) == null)
                    {
                        categoriaPrincipalInexistente += $"{categoria}, ";
                    }
                }
                return string.IsNullOrEmpty(categoriaPrincipalInexistente);
            }
        }
    }
}
