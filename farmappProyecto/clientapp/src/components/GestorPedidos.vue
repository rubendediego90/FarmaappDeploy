<template>
  <v-container>
  <v-card>
      <v-toolbar color="blue" dark >
        <v-icon class="mr-1">
          mdi mdi-order-bool-descending-variant
        </v-icon>
        <v-card-title>Pedidos Pendientes</v-card-title>
      </v-toolbar>
      <v-data-table
        :headers="headers"
        :items="items"
        class="elevation-1 text-center"
        :loading="loading"
        :search="search"
      >
          <template v-slot:item="props">
            <tr>
              <td class="text-center">{{ props.item.fechaPedidoString }}</td>
              <td class="text-center">{{ props.item.nombreArticulo }}</td>
              <td class="text-center">{{ props.item.cantidadPedido }}</td>
              <td class="text-center">{{ props.item.nombreProveedor}}</td>
              <td>
                <v-icon medium class="mr-2" color='green' @click="editItem(props.item,1)">check</v-icon>
                <v-icon medium class="mr-2" color='red' @click="editItem(props.item,0)">delete</v-icon>
              </td>
          </tr>
          </template>
      <template v-slot:top>
        <v-toolbar flat>
          <v-spacer></v-spacer>
          <v-text-field
            v-model="search"
            append-icon="mdi-magnify"
            label="Buscar pedido"
            single-line
            hide-details
          ></v-text-field>
          <v-spacer></v-spacer>
          <v-dialog v-model="dialogRecep" max-width="500px">
            <v-card>
              <v-card-title class="headline ">¿Deseas recepcionar el artículo?</v-card-title>
              <v-card-text>El artículo a recepcionar es: {{editedItem.nombreArticulo}}</v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="save()">Recepcionar</v-btn>
                <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-dialog v-model="dialogDel" max-width="500px">
            <v-card>
              <v-card-title class="headline ">¿Deseas anular el pedido?</v-card-title>
              <v-card-text>El artículo para anular es: {{editedItem.nombreArticulo}}</v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="deleteArt()">Borrar</v-btn>
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
  
  import PedidosServicios from '../servicios/PedidosServicios';
  import msgService from '../servicios/msgService'; 
  export default {
    data: () => ({
      items : [],
      dialogRecep: false,
      dialogDel: false,
      loading: true,

      headers: [
        {text: 'Fecha Pedido',value: 'fechaPedidoString',align: 'center'},
        { text: 'Nombre', value: 'nombreArticulo',align: 'center' },
        { text: 'Cantidad', value: 'cantidadPedido',align: 'center',  filterable: false },
        { text: 'Proveedor', value: 'nombreProveedor',align: 'center' },
        { text: 'Opciones', value: 'actions',align: 'center' , sortable: false ,  filterable: false },
      ],
      search : '',

      editedItem: {
        lineaId:0,
        cantidadPedido:0,
        estado:'',
        articuloId:0,
        fechaPedido:'',
        fechaPedidoString:'',
        nombreArticulo:'',
        nombreProveedor:'',
        precioCompra:0.0,
        proveedorId:0
      },
    }),

    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'Nuevo Stock' : 'Actualizar Stock'
      },
    },
    watch: {
      dialog (val) {
        val || this.close()
      },
      dialogDelete (val) {
        val || this.closeDelete()
      },
      options: {
        handler () {
          this.getDataFromApi()
        },
        deep: true,
      },
    },
    created () {
      this.getPedido()
    },
    methods: {
        getPedido () {
          this.loading=true;
          PedidosServicios.getPed().then((result) => {
            this.items = result.data
            this.loading = false
          }).catch((e) => {
            console.log(e)
          })
      },

      editItem (item,accion) {
        this.editedItem.lineaId=item.lineaId
        this.editedItem.cantidadPedido=item.cantidadPedido
        this.editedItem.estado=item.estado
        this.editedItem.articuloId=item.articuloId
        this.editedItem.proveedorId=item.proveedorId
        this.editedItem.precioCompra=item.precioCompra
        this.editedItem.fechaPedido=item.fechaPedido
        this.editedItem.fechaPedidoString=item.fechaPedidoString
        this.editedItem.nombreArticulo=item.nombreArticulo
        this.editedItem.nombreProveedor=item.nombreProveedor

        if(accion==1){
          this.dialogRecep= true
        }
        if(accion==0){
          this.dialogDel= true
        }
      },

      deleteArt(){
        var idBorrado = this.editedItem.lineaId
        PedidosServicios.deletePed(idBorrado).then((result) => {
            var msg= "Se ha borrado el pedido"
            this.$toast.success(msg);
            this.loading = false
            this.getPedido()
            this.close()
        }).catch((e) => {
            console.log(e)
          })
        
        this.close()
      },

      close () {
        this.dialog = false,
        this.dialogRecep = false,
        this.dialogDel= false,
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },
  
      save () {
        var data = {
          'lineaId': this.editedItem.lineaId,
          'cantidadPedido': this.editedItem.cantidadPedido,
          'estado':this.editedItem.estado,
          'articuloId':this.editedItem.articuloId,
          'fechaPedido':this.editedItem.fechaPedido,
          'precioCompra':this.editedItem.precioCompra,
          'proveedorId':this.editedItem.proveedorId,
          'fechaPedidoString':this.editedItem.fechaPedidoString,
          'nombreArticulo':this.editedItem.nombreArticulo,
          'nombreProveedor':this.editedItem.nombreProveedor,                     
        }
        this.loading=true;
        PedidosServicios.updatePed(data).then((result) => {
            var msg= "Se ha recepcionado el pedido de "+data.nombreArticulo
            this.$toast.success(msg);
            this.loading = false
            this.getPedido()
            this.close()
        }).catch((e) => {
            console.log(e)
          })
        
        this.close()
      },
    }
    
  }
</script>