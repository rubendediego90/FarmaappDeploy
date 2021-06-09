import axios from 'axios'

const VentaServicios = {

    createVenta() {
        return axios.post('api/Venta/').then((response) => {
            return response
        })
    },

    deleteRegistro(idBorrado) {
        return axios.delete('api/Venta/'+idBorrado).then((response) => {
            return response
        })
    },

    readRegistro() {
        return axios.get('api/Venta/').then((response) => {
            return response
        })
    },

    getSum(){
        return axios.get('api/Venta/getSumma').then((response) => {
            return response
        })
    },

    createRegistro(data) {
        return axios.post('api/Venta/CrearRegistro',data).then((response) => {
            return response
        })
    },

    updateRegistro(data){
        return axios.put('api/Venta/',data).then((response) => {
            return response
        })
    }
}
export default VentaServicios