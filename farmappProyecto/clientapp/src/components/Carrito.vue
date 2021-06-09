<template>
    <v-container>
        <v-card>
            <v-toolbar color="green" dark>
                <v-icon class="mr-1">
                shopping_cart
                </v-icon>
                <v-card-title>Carrito</v-card-title>
                
                <v-col class="text-right">
                <v-btn class="green--text" color="white" @click="sendVenta()">Finalizar Venta</v-btn>
                </v-col>

            </v-toolbar>

            
            <v-row>
              <v-col><v-card-text class="text--primary" style="text-align:right;font-weight: bold">Precio Total:{{carroSuma}}€</v-card-text></v-col>
            </v-row>
            <v-data-table
                :headers="headers"
                :items="items"
                class="elevation-1 text-center"
                :search="search"
                :options.sync="options"
            >
              <template v-slot:item="props">
                  <tr>
                      <td class="text-center">{{ props.item.nombreArticulo }}</td>
                      <td class="text-center">{{ props.item.precio }}€</td>
                      <td class="text-xs-left">{{ props.item.cantidad}}</td>
                      <td>
                          <v-icon medium class="mr-2" color='red' @click="deleteVenta(props.item.id)">delete</v-icon>
                          <v-icon medium class="mr-2" color='blue' @click="editVenta(props.item)">edit</v-icon>                    
                      </td>
                  </tr>
              </template>
            </v-data-table>
        </v-card>
        <v-dialog
        v-model="dialogEditVenta"
        max-width="400px"
        >
          <v-card>
              <v-form ref="form" v-model="valid" lazy-validation>
                  <v-card-text>
                      <p>¿Qué cantidad de {{editedItem.nombreArticulo}} ?</p>
                      <v-row no-gutters>
                          <v-col sm="6" md="6">
                              <v-text-field 
                              v-model="editedItem.cantidadPedido" 
                              label="Cantidad"
                              :rules="[
                                  v => !!v || 'Campo Obligatorio',
                                  v => Number.isInteger(Number(v)) || 'Debe ser una cantidad entera',
                                  v =>v >0 || 'Debe ser mayor de 0',
                                  v =>v <= editedItem.cantidadStock || 'Debe ser menor que la cantidad de Stock ' + editedItem.cantidadStock + ' unidades.'
                              ]"
                              required
                              type="number"
                              ></v-text-field>
                          </v-col>
                      </v-row>
                  </v-card-text>
              </v-form>
              <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="editNumVenta()">Guardar</v-btn>
                  <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
                  <v-spacer></v-spacer>
              </v-card-actions>
          </v-card>
      </v-dialog>
    </v-container>
</template>

<script>
import VentaServicios from '../servicios/VentaServicios';
import StockServicios from '../servicios/StockServicios';
import msgService from '../servicios/msgService'; 

  export default {
    name: 'BuscadorVenta',
    data: () => ({
     items : [],

    dataVenta:[],

     valid:false,

     currentPage: 1,

     carroSuma:0,
     

     options:{},

     valid:false,

      dialog: false,
      dialogEditVenta:false,

      loading: true,
      headers: [
        { text: 'Nombre', value: 'nombreArticulo',align: 'center'},
        { text: 'Precio/Unidad', value: 'precio',align: 'center' },
        { text: 'Unidades', value: 'cantidadStock',align: 'center' ,sortable: false },
        { text: 'Opciones', value: 'actions',align: 'center', sortable: false },
      ],

      search : '',

      idBorrado:0,

      editedIndex: -1,
      editedItem: {
        id:'',
        cantidadPedido:1,
        precio:0.0,
        nombreArticulo:0,
        cantidadStock:0,
      },
    }),

    watch: {
      dialog (val) {
        val || this.close()
      },
      dialogDelete (val) {
        val || this.closeDelete()
      },
    },
    created () {
      this.options.itemsPerPage=10
      this.getList()
      this.getSum()
    },
    methods: {
      getList () {
          VentaServicios.readRegistro().then((result) => {
            this.items = result.data

          }).catch((e) => {
            console.log(e)
          })
        },

      getSum(){
        VentaServicios.getSum().then((result) => {
            this.carroSuma = result.data
            //this.reloadPage();
          }).catch((e) => {
            console.log(e)
          })
        },

      editNumVenta(){
          var data = {
            'cantidad': this.editedItem.cantidadPedido,
            'articuloId': this.editedItem.articuloId,
            'precio':this.editedItem.precio,
            'nombreArticulo':this.editedItem.nombreArticulo,
            'id':this.editedItem.id
            }

        VentaServicios.updateRegistro(data).then((result) => {
            this.items = result.data
            this.reloadPage();
          }).catch((e) => {
            console.log(e)
          })
      },

          handlePageChange(value) {
      this.currentPage = value;
    },

      editVenta(valor){
        this.editedItem.id=valor.id
        this.editedItem.nombreArticulo=valor.nombreArticulo
        this.editedItem.cantidadPedido=valor.cantidad
        this.editedItem.precio=valor.precio
        this.editedItem.articuloId=valor.articuloId

        StockServicios.getStockId(valor.articuloId).then((result) => {

            this.editedItem.cantidadStock=result.cantidad
            this.dialogEditVenta=true
          }).catch((e) => {
            console.log(e)
          })
      },

      deleteVenta(idBorrado){
          VentaServicios.deleteRegistro(idBorrado).then((result) => {
            this.getList()
            this.getSum()
          }).catch((e) => {
            console.log(e)
          })
        },

      sendVenta(){
          VentaServicios.createVenta().then((response) => {
            this.getList()
            this.close()
            var msg= "Venta realizada con éxito. Total: "+response.data+"€"
            this.$toast.success(msg);
            setTimeout(() => {this.reloadPage()}, 2000);
                  
          }).catch((e) => {
            this.$toast.error(e);
          })
      },

      reloadPage(){
        window.location.reload()
      },

      close () {
        this.dialogEditVenta = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
          this.editedItem.cantidadPedido=1
        })
      },
    },
  }
</script>