<template>
  <v-app>
    <v-app-bar fixed dense color="primary" dark>
        <v-spacer />
            <v-app-bar-title>
              <v-card color="primary">
                <v-card-text class="font-weight-bold basil--text white--text">
                  <h2>Farmapp</h2>
                </v-card-text>
              </v-card>
            </v-app-bar-title>
        <v-spacer />
        <template v-slot:extension>
          <v-tabs
            v-model="tab"
            background-color="purple"
            dark
            grow
            icons-and-text
            color="blue"
            slider-color="blue"
          >
            <v-tab v-for="(tab, i) in items" :key="i" :to="tab.link" exact light active-class="color: green;">
              <v-icon>{{ tab.icon }}</v-icon>
              {{ tab.text }}
            </v-tab>
          </v-tabs>
        </template>
      </v-app-bar>
      <div style="padding-top:100px ">
        <router-view />
      </div>
  </v-app>
</template>

<script>
  export default {
    name: 'App',
    components:{
      },
    data: () => ({
      drawer: false,
        tab: 1,
        items: [
          { text: 'Ventas', icon: 'mdi mdi-basket-outline', link: 'Venta'},
          { text: 'Pedidos', icon: 'mdi mdi-truck', link: 'Pedidos' },
          { text: 'Stock', icon: 'store', link: 'Stock' },
          { text: 'Articulos', icon: 'mdi mdi-pill mdi', link: 'Articulos' },
          { text: 'Proveedores', icon: 'mdi mdi-card-account-phone-outline', link: 'Proveedores' },
        ],
    }),

  };
</script>


