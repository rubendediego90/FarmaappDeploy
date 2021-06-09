import axios from 'axios'

const ProveedorServicios = {

getProv() {
    return axios.get('api/Proveedores/GetProveedor').then((response) => {
      return response.data
    })
  },
createProv(data) {
    return axios.post('api/Proveedores/',data).then((response) => {
      return response
    })
  },
updateProv(data) {
    return axios.put('/api/Proveedores/'+data.proveedorId,data).then((response) => {
      return response.data
    })
  },
}
export default ProveedorServicios