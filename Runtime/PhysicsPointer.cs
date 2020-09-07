using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JitzuTools
{
    
    [RequireComponent(typeof(LineRenderer))]
    public class PhysicsPointer : MonoBehaviour
    {
        public LineRenderer linea;
        [Tooltip("Longitud maxima de deteccion de elementos")]
        public float longitudDefault = 3.0f;


        private void Awake()
        {
            linea = GetComponent<LineRenderer>();
        }


        private void Update()
        {
            ActualizarLongitud();
        }
        private void ActualizarLongitud()
        {
            linea.SetPosition(0, transform.position);
            linea.SetPosition(1, CalcularFinal());
        }

        private Vector3 CalcularFinal()
        {
            RaycastHit hit = RayoForward();
            Vector3 posicionFinal = FinDefault(longitudDefault);

            if (hit.collider)
                posicionFinal = hit.point;

            return posicionFinal;
        }

        /// <summary>
        /// Crea un RaycastHit en direccion forward del objeto que tenga este script
        /// con distancia de longitudDefault = 3m
        /// </summary>
        /// <returns></returns>
        private RaycastHit RayoForward()
        {
            RaycastHit hit;
            Ray rayo = new Ray(transform.position, transform.forward);

            Physics.Raycast(rayo, out hit, longitudDefault);

            return hit;
        }


        /// <summary>
        /// Regresa un punto Vector3 de acuerdo a la posicion de este script hacia z 
        /// el largo de Z es dado por el parametro longitud
        /// </summary>
        /// <param name="longitud"></param>
        /// <returns></returns>
        private Vector3 FinDefault(float longitud)
        {
            return transform.position + (transform.forward * longitud);

        }

    }

}
