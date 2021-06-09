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
            <td class="text-center">{{ props.item.proveedorId }}</td>
            <td class="text-center">{{ props.item.nombre }}</td>
            <td class="text-center">{{ props.item.telefono }}</td>
            <td class="text-center">{{ props.item.email}}</td>
            <td class="text-center">{{ props.item.direccion}}</td>
            <td><v-icon medium class="mr-2" color='blue' @click="editItem(props.item)">edit</v-icon></td>
        </tr>
        </template>
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Proveedores</v-toolbar-title>
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
          max-width="650px"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">Nuevo Proveedor</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
            <v-form ref="form" v-model="valid">
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="9" md="9">
                    <v-text-field 
                    v-model="editedItem.nombre" 
                    label="Nombre Proveedor"
                    :rules="[v => !!v || 'Campo Obligatorio']"
                    required
                    prepend-icon="mdi mdi-card-account-phone-outline"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="3" md="3">
                    <v-text-field 
                    v-model="editedItem.telefono" 
                    label="Teléfono"
                    prepend-icon="mdi mdi-phone"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field 
                      v-model="editedItem.direccion" 
                      label="Dirección" 
                      prepend-icon="mdi mdi-city-variant-outline"
                      ></v-text-field>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field 
                      v-model="editedItem.email"
                      label="Email"
                      type="email"
                      prepend-icon="mdi mdi-at"
                      :rules="[v => /.+@.+\..+/.test(v) || 'El email debe ser válido']"
                      required
                      ></v-text-field>
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
            <v-card-text>El proveedor seleccionado es {{editedItem.nombre}}</v-card-text>
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
  import ProveedorServicios from '../servicios/ProveedorServicios';
  export default {
    data: () => ({
      items : [],

      valid: false,
      dialog: false,
      dialogAct: false,
      loading: true,
      headers: [
        { text: 'Estado', value: 'estado',align: 'center' , filterable: false},
        {text: 'Id Proveedor',align: 'center',value: 'proveedorId', filterable: false},
        { text: 'Nombre', value: 'nombre',align: 'center' },
        { text: 'Telefono', value: 'telefono',align: 'center', filterable: false },
        { text: 'Email', value: 'email',align: 'center', filterable: false },
        { text: 'Direccion', value: 'direccion',align: 'center', filterable: false },
        { text: 'Opciones', value: 'actions',align: 'center', sortable: false },
      ],
      search : '',
   
      editedIndex: -1,

      editedItem: {
        proveedorId:'',
        nombre:'',
        telefono:'',
        email:'',
        direccion:'',
        estado:true,
      },

      defaultItem:{
        proveedorId:'',
        nombre:'',
        telefono:'',
        email:'',
        direccion:'',
        estado:true,      
      }

    }),

    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'Nuevo Proveedor' : 'Actualizar Proveedor'
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
          this.getProveedor()
        },
        deep: true,
      },
    },
    created () {
      this.getProveedor()
      
    },
    methods: {
      getProveedor() {
          this.loading=true;
          ProveedorServicios.getProv().then((result) => {
            this.items = result
            this.loading = false
          }).catch((e) => {
            //this.$toast.error(e);
          })
      },

      editItem (item) {
        this.editedItem.proveedorId=item.proveedorId
        this.editedItem.nombre=item.nombre
        this.editedItem.telefono=item.telefono
        this.editedItem.email=item.email
        this.editedItem.direccion=item.direccion
        this.editedItem.estado=item.estado
        this.editedIndex=1
        this.dialog = true
      },
      act_desact(item){
        this.editedItem.proveedorId=item.proveedorId
        this.editedItem.nombre=item.nombre
        this.editedItem.telefono=item.telefono
        this.editedItem.email=item.email
        this.editedItem.direccion=item.direccion
        this.editedItem.estado=item.estado
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
            
            'nombre': this.editedItem.nombre,
            'telefono': this.editedItem.telefono,  
            'email':this.editedItem.email,        
            'direccion':this.editedItem.direccion,
            'estado':this.editedItem.estado
          }
          if (this.editedIndex > -1) {
            data.proveedorId = this.editedItem.proveedorId;
         
            this.loading=true;
            ProveedorServicios.updateProv(data).then((result) => {
              this.getProveedor()
              this.close()
              this.loading=false
              var msg= "Se ha actualizado el Proveedor " +data.nombre
              this.$toast.success(msg);
            }).catch(function(e){
              this.$toast.error(e);
            });}
          else {
            this.loading=true;
            ProveedorServicios.createProv(data).then((result) => {
              this.loading = false
              this.getProveedor()
              this.close()
              
              var msg= "Se ha creado el Proveedor " +data.nombre
              this.$toast.success(msg);
            }).catch((e) => {
              this.$toast.error(e);
            })
          }
          this.getProveedor()
          this.close()
        }
      },
    },
  }
</script>
