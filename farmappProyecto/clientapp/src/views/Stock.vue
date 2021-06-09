<template>
  <v-container>
    <v-data-table
      :headers="headers"
      :items="items"
      class="elevation-1 text-center"
      :loading="loading"
      :search="search"
    >
         <template v-slot:item="props">
          <tr>
            <td class="text-center">{{ props.item.articuloId }}</td>
            <td class="text-center">{{ props.item.nombre }}</td>
            <td class="text-center">{{ props.item.descripcion }}</td>
            <td class="text-center">{{ props.item.cantidad}}</td>
            <td><v-icon medium class="mr-2" color='blue' @click="editItem(props.item)">edit</v-icon></td>
        </tr>
        </template>
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Stock</v-toolbar-title>
        <v-divider
          class="mx-4"
          inset
          vertical
        ></v-divider>
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Buscar"
          single-line
          hide-details
        ></v-text-field>
        <v-spacer></v-spacer>
        <v-dialog
          v-model="dialog"
          max-width="500px"
        >
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-form ref="form" v-model="valid" lazy-validation>
                <v-row no-gutters>
                  <v-col cols="12" sm="12" md="16">
                    <v-text-field 
                    v-model="editedItem.nombre" 
                    label="Nombre Articulo" 
                    readonly
                    prepend-icon="mdi mdi-pill"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field
                      v-model="editedItem.cantidad"
                      prepend-icon="mdi mdi-numeric"
                       label="Cantidad"
                       type="number"
                       :rules="[  v => Number.isInteger(Number(v)) || 'Debe ser una cantidad entera',
                                  v => v >= 0  || 'No puede ser negativo'
                       ]"
                        ></v-text-field>
                  </v-col>
                </v-row>
                <v-row no-gutters>
                  <v-col cols="12" sm="12" md="16">
                    <v-textarea
                      label="Descripción"
                      prepend-icon="mdi mdi-book-open-outline"
                      v-model="editedItem.descripcion"
                      readonly
                    ></v-textarea>
                  </v-col>
                </v-row>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
              <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
  </v-data-table>
</v-container>
</template>

<script>
  
  import StockServicios from '../servicios/StockServicios';
  export default {
    data: () => ({
      valid: false,
      items : [],
      dialog: false,
      dialogAct: false,
      loading: true,
      headers: [
        {text: 'Id Articulo',value: 'articuloId',align: 'center'},
        { text: 'Nombre', value: 'nombre',align: 'center' },
        { text: 'Descripción', value: 'descripcion',align: 'center' },
        { text: 'Cantidad', value: 'cantidad',align: 'center' },
        { text: 'Opciones', value: 'actions',align: 'center' , sortable: false },
      ],
      search : '',

      editedIndex: -1,
      editedItem: {
        stockId:'',
        articuloId:'',
        nombre:'',
        email:'',
        estado:'',
        cantidad:'',
        descripcion:''
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
      this.stockList()
    },
    methods: {
        stockList () {
          this.loading=true;
          StockServicios.getStock().then((result) => {
            this.items = result
            this.loading = false
          }).catch((e) => {
            console.log(e)
          })
      },

      editItem (item) {
        
        this.editedItem.stockId=item.stockId
        this.editedItem.articuloId=item.articuloId
        this.editedItem.nombre=item.nombre
        this.editedItem.estado=item.estado
        this.editedItem.cantidad=item.cantidad
        this.editedItem.descripcion=item.descripcion
        
        this.editedIndex=1
        this.dialog = true
      },

      close () {
        this.dialog = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },
      closeDesAct () {
        this.dialogAct=false;
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },
      save () {
        if(this.$refs.form.validate()){
            var data = {
              'stockId': this.editedItem.stockId,
              'articuloId': this.editedItem.articuloId,
              'nombre':this.editedItem.nombre,
              'estado':this.editedItem.estado,
              'cantidad':this.editedItem.cantidad,
            }
            
            this.loading=true;
            StockServicios.updateStock(data).then((result) => {
                this.loading = false
                this.stockList()
                this.close()
            }).catch((e) => {
                console.log(e)
              })
            
            this.close()
        }
      },
    },
  }
</script>
