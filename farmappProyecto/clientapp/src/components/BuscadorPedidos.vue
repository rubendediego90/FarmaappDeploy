<template>
    <v-container>
        <v-card>
            <v-toolbar color="blue" dark >
              <v-icon class="mr-1">
                list
              </v-icon>
              <v-card-title>Listado de Artículos</v-card-title>
            </v-toolbar>
            <v-data-table
              :headers="headers"
              :items="items"
              class="elevation-1 text-center"
              :loading="loading"
              :search="search"
              hide-default-footer
              :options.sync="options"
            >
                <template v-slot:item="props">
                  <tr>
                    <td class="text-center">{{ props.item.nombreArticulo }}</td>
                    <td class="text-center">{{ props.item.precio }}€</td>
                    <td class="text-xs-left">{{ props.item.cantidadStock}}</td>
                    <td class="text-center">{{ props.item.nombreProveedor}}</td>
                    <td><v-icon medium class="mr-2" color='blue' @click="editItem(props.item)">add</v-icon></td>
                </tr>
                </template>
            <template v-slot:top>
              <v-toolbar flat>
                  <v-spacer></v-spacer>
                  <v-text-field
                    v-model="search"
                    append-icon="mdi-magnify"
                    label="Busca el artículo hacer el pedido"
                    single-line
                    hide-details
                  ></v-text-field>
                  <v-spacer></v-spacer>
              <v-dialog
                  v-model="dialog"
                  max-width="400px"
                >
                  <v-card>
                    <v-form ref="form" v-model="valid" lazy-validation>
                      <v-card-text>
                        <p>¿Qué cantidad deseas pedir de {{editedItem.nombreArticulo}} ?</p>
                        <v-row no-gutters>
                        <v-col sm="6" md="6">
                            <v-text-field 
                              v-model="editedItem.cantidadPedido" 
                              label="Cantidad Pedido"
                              :rules="[
                                v => !!v || 'Campo Obligatorio',
                                v => Number.isInteger(Number(v)) || 'Debe ser una cantidad entera',
                                v =>v >0 || 'Debe ser mayor de 0'
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
                      <v-btn color="blue darken-1" text @click="save(0)">Guardar</v-btn>
                      <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
                      <v-spacer></v-spacer>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-toolbar>
            </template>
          </v-data-table>
      </v-card>
</v-container>
</template>

<script>
import ArticuloServicios from '../servicios/ArticuloServicios';
import PedidosServicios from '../servicios/PedidosServicios';
import msgService from '../servicios/msgService'; 

  export default {
    name: 'BuscadorPedidos',
    data: () => ({
     items : [],
     valid:false,

     options:{},

      dialog: false,
      loading: true,
      headers: [
        { text: 'Nombre', value: 'nombreArticulo',align: 'center' },
        { text: 'Precio', value: 'precio',align: 'center',  filterable: false },
        { text: 'Cantidad', value: 'cantidadStock',align: 'center' ,  filterable: false},
        { text: 'Proveedor', value: 'nombreProveedor',align: 'center', sortable: false,  filterable: false },
        { text: 'Opciones', value: 'actions', sortable: false,  filterable: false },
      ],
      search : '',

      idBorrado:0,
     
      editedIndex: -1,
      editedItem: {
        articuloId:'',
        nombreArticulo:'',
        cantidadPedido:0,
        precioCompra:0.0,
        proveedorId:0
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
      this.options.itemsPerPage=5
      this.getList()
    },
    methods: {
        getList () {
          this.loading=true;
          ArticuloServicios.getListOrd().then((result) => {
            this.items = result.data
            this.loading = false
            
          }).catch((e) => {
            console.log(e)
          })
      },
      editItem (item) {
        this.editedItem.articuloId=item.articuloId
        this.editedItem.nombreArticulo=item.nombreArticulo
        this.editedItem.proveedorId=item.proveedorId
        this.editedItem.precioCompra=item.precio     
        this.dialog = true
        },

      save(){
        if(this.$refs.form.validate()){
          var data = {
            'cantidadPedido': this.editedItem.cantidadPedido,
            'articuloId': this.editedItem.articuloId,
            'proveedorId':this.editedItem.proveedorId,
            'estado':this.editedItem.estado,
            'precioCompra':this.editedItem.precioCompra,
          }

          this.loading=true;
          PedidosServicios.createPed(data).then((result) => {
            this.loading = false
            this.getList()
            this.close()
            var msg= "Pedido realizado con exito "
            this.$toast.success(msg);
            setTimeout(() => {this.reloadPage()}, 2000);
            
  
          }).catch((e) => {
            this.$toast.error(e);
          })
        }
      },
      
      reloadPage(){
        
        window.location.reload()
      },

      close () {
        this.dialog = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },
    },
  }
</script>



