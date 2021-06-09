import axios from 'axios'

const ArticuloServicios = {

getArt() {
    return axios.get('api/Articulos/Listar').then((response) => {
      return response.data
    })
  },
createArt(data) {
    return axios.post('api/Articulos/',data).then((response) => {
      return response
    })
  },
updateArt(data) {
    return axios.put('/api/Articulos/'+data.articuloId,data).then((response) => {
      return response.data
    })
  },
  getProSelect() {
    return axios.get('/api/Proveedores/SelectProveedor').then((response) => {
      return response
    })
  },
  getListOrd(){
    return axios.get('/api/Articulos/ListarPedido').then((response) => {
      return response
    })
  },
}
export default ArticuloServicios