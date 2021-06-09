<template>
    <v-list nav dense dark color="primary" v-model="selectedItem">
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.link"
        >
          <v-list-item-icon>
            <v-icon v-text="item.icon"></v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="item.text"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
    </v-list>
</template>

<script>
  export default {
    name: 'BarraNavegacion',
    data: () => ({
      drawer: false,
      selectedItem: 1,
      items: [
        { text: 'Ventas', icon: 'mdi mdi-basket-outline', link: 'Venta'},
        { text: 'Pedidos', icon: 'mdi mdi-truck', link: 'Pedidos' },
        { text: 'Stock', icon: 'store', link: 'Stock' },
        { text: 'Articulos', icon: 'mdi mdi-pill mdi', link: 'Articulos' },
        { text: 'Proveedores', icon: 'mdi mdi-card-account-phone-outline', link: 'Proveedores' },
      ],
    }),
  }
</script>