using UnityEngine;

namespace JitzuTools.DebugColor
{
    /// <summary>
    /// Formatea el texto en consola para que sea mas visible durante el desarrollo
    /// ej:
    ///    Una linea comun con el texto en BOLD
    ///     Debug.Log(DebugColor.Bold("hola") + " otro texto"); 
    ///     Combinacion de todas las posibilidades, se anidan sin importar el orden
    //      Debug.Log(DebugColor.Italica( DebugColor.Size( DebugColor.Color(DebugColor.Bold("hola"),"red"),"20"))   + " otro texto");
    /// </summary>
    public static class DebugColor 
    {
        /// <summary>
        /// Cambia el texto a BOLD, se puede combinar
        /// </summary>
        /// <param name="_texto"></param>
        /// <returns></returns>
        public static string Bold(this string _texto)
        {
            return $"<b>{_texto}</b>";
        }
        /// <summary>
        /// Cambia de color un texto, puede combinarse con los demas metodos
        /// </summary>
        /// <param name="_texto">texto a cambiar color</param>
        /// <param name="_color">colores inlcuidos(red,gree,blue,...) o valor hexadecimas("#000000")</param>
        /// <returns></returns>
        public static string Color(this string _texto, string _color)
        {
            return $"<color={_color}>{_texto}</color>";
        }
        /// <summary>
        /// Cambia de tamaño el texto en la consola, se puede combinar con otros metodos
        /// </summary>
        /// <param name="_texto">texto a cambiar</param>
        /// <param name="_size">tamaño en string ej: "16" </param>
        /// <returns></returns>
        public static string Size(this string _texto,string _size)
        {
            return $"<size={_size}>{_texto}</size>";
            
        }

        /// <summary>
        /// Cambia el texto a italicas, se puede combinar
        /// </summary>
        /// <param name="_texto"></param>
        /// <returns></returns>
        public static string Italica(this string _texto)
        {
            return $"<i>{_texto}</i>";
        }

        /// <summary>
        /// Imprime en la consola un mensaje que consideras un ERROR, el texto que introduces puede ser modificado con los demas
        /// metodos, solo aparece "ERROR:" en rojo,bold
        /// </summary>
        /// <param name="_texto">texto que aperece delante de ERROR:</param>
        /// <returns></returns>
        public static string Error(this string _texto)
        {
            Debug.Log($"{Color(Bold("ERROR: "), "red")}{_texto}");
            return null;
        }
        /// <summary>
        /// Imprime en la ocnsola unmensaje que consideras una ALERTA, el texto que introduces puede ser modificado con mas
        /// estilos con los diferentes metodos(BOLD,Color,Size)
        /// </summary>
        /// <param name="_texto">tu texto que aparece delante de ALERTA:</param>
        /// <returns></returns>
        public static string Alerta(this string _texto)
        {
            Debug.Log($"{Color(Bold("Alerta: "), "yellow")}{_texto}");
            return null;
        }
        public static string Mensaje(this string _texto)
        {
             Debug.Log("Hola");
             return null;
        }
    }
}
