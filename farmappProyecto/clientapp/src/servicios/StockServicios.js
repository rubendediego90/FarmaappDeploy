import axios from 'axios'

const StockServicios = {

getStock() {
    return axios.get('api/Stock/GetStock').then((response) => {
      return response.data
    })
  },

getStockId(idArt){
  return axios.get('api/Stock/'+idArt).then((response) => {
    return response.data
  })
},

updateStock(data) {
    return axios.put('/api/Stock/'+data.stockId,data).then((response) => {
      return response.data
    })
  },
}
export default StockServicios