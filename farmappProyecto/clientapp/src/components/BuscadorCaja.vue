<template>
    <v-container>
        <v-card>
            <v-toolbar color="green" dark >
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
                    <td><v-icon medium class="mr-2" color='blue' @click="editItem(props.item)">add</v-icon></td>
                </tr>
            </template>
              <template v-slot:top>
                  <v-toolbar flat>
                      <v-spacer></v-spacer>
                      <v-text-field
                        v-model="search"
                        append-icon="mdi-magnify"
                        label="Busca el artículo añadirlo a la caja"
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
                                                  v =>v <= editedItem.cantidadStock || 'Debe ser mayor que la cantidad de Stock ' + editedItem.cantidadStock + ' unidades.'
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
import VentaServicios from '../servicios/VentaServicios';
import msgService from '../servicios/msgService'; 

  export default {
    name: 'BuscadorVenta',
    data: () => ({
     items : [],

    dataVenta:[],

     valid:false,

     collapseOnScroll: true,

     options:{},

      dialog: false,
      loading: true,
      headers: [
        { text: 'Nombre', value: 'nombreArticulo',align: 'center'},
        { text: 'Precio', value: 'precio',align: 'center' },
        { text: 'Disponible', value: 'cantidadStock',align: 'center' ,sortable: false },
        { text: 'Opciones', value: 'actions',align: 'center', sortable: false },
      ],

      search : '',

      idBorrado:0,

      editedIndex: -1,
      editedItem: {
        articuloId:'',
        nombreArticulo:'',
        cantidadPedido:1,
        precioCompra:0.0,
        proveedorId:0,
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
        this.editedItem.cantidadStock=item.cantidadStock
        this.editedItem.proveedorId=item.proveedorId
        this.editedItem.precio=item.precio     
        this.dialog = true
        },

      save(){
        if(this.$refs.form.validate()){
            var data = {
                'cantidad': this.editedItem.cantidadPedido,
                'articuloId': this.editedItem.articuloId,
                'precio':this.editedItem.precio,
                'nombreArticulo':this.editedItem.nombreArticulo,
                
            }
          VentaServicios.createRegistro(data).then(() => {
            this.dialog=false
            this.reloadPage()
            
          }).catch((e) => {
            this.$toast.error("Error: Posiblemente el artículo esté en el carrito " + e);
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