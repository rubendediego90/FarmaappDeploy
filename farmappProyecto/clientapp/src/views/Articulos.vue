<template>
  <v-form ref="form" v-model="valid" lazy-validation>
    <v-data-table
      :headers="headers"
      :items="items"
      class="elevation-1 text-center"
      :loading="loading"
      :search="search"
    >
         <template v-slot:item="props">
          <tr>
            <td @click="act_desact(props.item)">
              <div v-if="props.item.estado">
                <span class="green--text">Activo</span>
              </div>
              <div v-else>
                <span class="red--text">Inactivo</span>
              </div>
            </td>
            <td class="text-center">{{ props.item.articuloId }}</td>
            <td class="text-center">{{ props.item.nombreArticulo }}</td>
            <td class="text-center">{{ props.item.descripcion }}</td>
            <td class="text-center">{{ props.item.precio}}€</td>
            <td class="text-center">{{ props.item.nombreProveedor}}</td>
            <td><v-icon medium class="mr-2" color='blue' @click="editItem(props.item)">edit</v-icon>
            </td>
        </tr>
        </template>
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Articulos</v-toolbar-title>
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
          max-width="600px"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">Nuevo Articulo</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-form ref="form" v-model="valid">
            <v-card-text>
              <v-container>
                <v-row no-gutters>
                  <v-col cols="12" sm="12" md="16">
                    <v-text-field 
                    v-model="editedItem.nombreArticulo"
                    label="Nombre Articulo"
                    prepend-icon="mdi mdi-pill"
                    :rules="[v => !!v || 'Campo Obligatorio']"
                    required
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row no-gutters>
                  <v-col cols="12" sm="12" md="16">
                    <v-select
                      v-model="editedItem.provObj"
                      :items="comboBoxProv"
                      prepend-icon="mdi mdi-card-account-phone-outline"
                      item-text="nombre"
                      item-value="proveedorId"
                      label="Proveedor"
                      :rules="[v => v.proveedorId > 0  || 'Campo Obligatorio']"
                      type="number"
                      required
                      return-object
                    />
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field 
                      v-model="editedItem.precio" 
                      prepend-icon="mdi mdi-currency-eur"
                      label="Precio" 
                      align="right" 
                      type="number"
                      suffix="€"
                      :rules="[
                        v => v > 0  || 'Mayor de 0',
                        v => /.+\.\d{2}$/.test(v) || 'Deben ser 2 decimales'
                      ]"
                      required
                      ></v-text-field>
                  </v-col>
                </v-row>
                <v-row no-gutters>
                  <v-col cols="12" sm="12" md="16">
                    <v-textarea
                      label="Descripción"
                      prepend-icon="mdi mdi-book-open-outline"
                      v-model="editedItem.descripcion"
                    ></v-textarea>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>
            </v-form>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="save(0)">Guardar</v-btn>
              <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogAct" max-width="500px">
          <v-card>
            <v-card-title class="headline ">¿Qué deseas hacer?</v-card-title>
            <v-card-text>El articulo seleccionado es {{editedItem.nombreArticulo}}</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="save(2)">Activar</v-btn>
              <v-btn color="blue darken-1" text @click="save(1)">Desactivar</v-btn>
              <v-btn color="blue darken-1" text @click="closeDesAct">Cancelar</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
  </v-data-table>
</v-form>
</template>

<script>
  import msgService from '../servicios/msgService';
  import ArticuloServicios from '../servicios/ArticuloServicios';
  export default {
    data: () => ({
      items : [],

      valid: false,
      dialog: false,
      dialogAct: false,
      loading: true,
      headers: [
        { text: 'Estado', value: 'estado',align: 'center',  filterable: false },
        {text: 'Id Articulo',value: 'ArticuloId',align: 'center', filterable: false},
        { text: 'Nombre', value: 'nombreArticulo',align: 'center' },
        { text: 'Descripcion', value: 'descripcion',align: 'center' },
        { text: 'Precio', value: 'precio',align: 'center' , filterable: false},
        { text: 'Proveedor', value: 'nombreProveedor',align: 'center', filterable: false },
        { text: 'Opciones', value: 'actions',align: 'center', sortable: false , filterable: false},
      ],
      search : '',

      comboBoxProv:[{text:'Sin dato',value:0}],

      idBorrado:0,
   
      editedIndex: -1,

      editedItem: {
        articuloId:'',
        nombreArticulo:'',
        descripcion:'',
        precio:0.0,
        provObj:{
          proveedorId:0,
          nombre:''
        },
        estado:true,
      },

      defaultItem:{
        articuloId:'',
        nombreArticulo:'',
        descripcion:'',
        precio:0.0,
        provObj:{
          proveedorId:0,
          nombre:''
        },
        estado:true,        
      }

    }),

    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'Nuevo Articulo' : 'Actualizar Articulo'
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
      this.getSelect()
      this.getArticulo()
      
    },
    methods: {
      getArticulo() {
          this.loading=true;
          ArticuloServicios.getArt().then((result) => {
            this.items = result
            this.loading = false
          }).catch((e) => {
            this.$toast.error(e);
          })
      },
      getProv(){
        ArticuloServicios.getProSelect().then((result)=>{
           this.comboBoxProv=result.data; 
        }).catch((e) => {
          this.$toast.error(e);
        })
      },
      getSelect(){
        this.getProv();
      },

      editItem (item) {
        this.getSelect()
        this.editedItem.articuloId=item.articuloId
        this.editedItem.nombreArticulo=item.nombreArticulo
        this.editedItem.precio=item.precio
        this.editedItem.descripcion=item.descripcion
        this.editedItem.provObj.proveedorId=item.proveedorId
        this.editedItem.provObj.nombre=item.nombreProveedor
        this.editedItem.estado=item.estado
        this.editedIndex=1
        this.dialog = true
      },
      act_desact(item){
        this.editedItem.articuloId=item.articuloId
        this.editedItem.nombreArticulo=item.nombreArticulo
        this.editedItem.precio=item.precio
        this.editedItem.descripcion=item.descripcion
        this.editedItem.estado=item.estado
        this.editedItem.provObj.proveedorId=item.proveedorId
        this.editedItem.provObj.nombre=item.nombreProveedor
        this.editedIndex=1
        this.dialogAct=true;
      },
      close () {
        this.dialog = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
        //this.refresh()
      },
      closeDesAct () {
        this.dialogAct=false;
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
        //this.refresh()
      },

    refresh(){
      window.location.reload()
    },
    
    save (estado) {
      if(this.$refs.form.validate()){
          if(estado==1){
            this.editedItem.estado=false;
            this.dialogAct=false;
          }
          if(estado==2){
            this.editedItem.estado=true;
            this.dialogAct=false;
          }

          var data = {
            'articuloId': parseInt(this.editedItem.articuloId),
            'nombreArticulo': this.editedItem.nombreArticulo,
            'proveedorId': this.editedItem.provObj.proveedorId,  
            'nombreProveedor':this.editedItem.provObj.nombre,        
            'descripcion':this.editedItem.descripcion,
            'precio':this.editedItem.precio,
            'estado':this.editedItem.estado
          }

          if (this.editedIndex > -1) {
            this.loading=true;
            ArticuloServicios.updateArt(data).then((result) => {
              this.getArticulo()
              this.close()
              this.loading=false
              var msg= "Se ha actualizado el artículo " +data.nombreArticulo
              this.$toast.success(msg);
            }).catch(function(e){
              this.$toast.error(e);
            });}
          else {
            this.loading=true;
            ArticuloServicios.createArt(data).then((result) => {
              this.loading = false
              this.getArticulo()
              this.close()
              
              var msg= "Se ha creado el artículo " +data.nombreArticulo
              this.$toast.success(msg);
            }).catch((e) => {
              this.$toast.error(e);
            })
          }
          this.getArticulo()
          this.close()
        }
      },
    },
  }
</script>

