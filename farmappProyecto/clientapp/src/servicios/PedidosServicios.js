import axios from 'axios'

const PedidosServicios = {

    createPed(data) {
        return axios.post('api/Pedidos/',data).then((response) => {
            return response
        })
    },

    updatePed(data) {
        return axios.put('api/Pedidos/',data).then((response) => {
            return response
        })
    },

    deletePed(idBorrado) {
        return axios.delete('/api/Pedidos/'+idBorrado).then((response) => {
          return response.data
        })
    },

    getPed(){
        return axios.get('api/Pedidos/').then((response) => {
            return response
        })
    }
}
export default PedidosServicios