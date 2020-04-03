<template>
  <q-table
    :dense="$q.screen.lt.sm"
    title="Produtos"
    :data="products"
    :filter="filter"
    :columns="columns"
    :separator="config.separator"
    :rows-per-page-label="config.rowsPerPageLabel"
    selection="single"
    :selected.sync="selected"
    row-key="id"
  >
    <template v-slot:top>
      <div v-if="selected.length > 0">
        <q-btn round color="grey" icon="edit" :disable="config.disable" @click="edit(selected[0])" />
        <q-btn
          round
          color="negative"
          icon="clear"
          class="q-ml-sm"
          :disable="config.disable"
          @click="alertOnDelete(selected[0])"
        />
      </div>
      <div v-else>
        <div class="text-h6">Produtos</div>
      </div>
      <q-space />
      <q-input borderless dense debounce="300" color="primary" v-model="filter">
        <template v-slot:append>
          <q-icon name="search" />
        </template>
      </q-input>
    </template>

    <template v-slot:body="props">
      <q-tr :props="props">
        <q-td auto-width>
          <q-checkbox color="secondary" v-model="props.selected" />
        </q-td>
        <q-td key="id" :props="props">{{ props.row.id }}</q-td>
        <q-td key="name" :props="props">{{ props.row.name }}</q-td>
        <q-td key="quantityInPackage" :props="props">{{ props.row.quantityInPackage }}</q-td>
        <q-td key="unitOfMeasurement" :props="props">{{ props.row.unitOfMeasurement }}</q-td>
        <q-td key="price" :props="props">{{ props.row.price | formatMoney }}</q-td>
      </q-tr>
    </template>
  </q-table>
</template>

<script>
export default {
  name: "ProductDialog",
  props: ["products"],
  data() {
    return {
      filter: "",
      columns: [
        {
          name: "id",
          required: true,
          label: "ID",
          align: "center",
          field: row => row.id,
          sortable: true
        },
        {
          name: "name",
          required: true,
          label: "Nome",
          align: "left",
          field: row => row.name,
          format: val => `${val}`,
          sortable: true
        },
        {
          name: "quantityInPackage",
          align: "center",
          label: "Quantidade",
          field: "quantityInPackage",
          sortable: true
        },
        {
          name: "unitOfMeasurement",
          label: "UN",
          field: "unitOfMeasurement",
          sortable: true
        },
        {
          name: "price",
          label: "Preço",
          field: row => row.price
        }
      ],
      config: {
        separator: "cell", // horizontal, vertical, cell and none
        rowsPerPageLabel: "Registros por página",
        disable: true
      },
      selected: []
    };
  },
  watch: {
    selected(value) {
      if (value.length > 0) this.config.disable = false;
    }
  },
  methods: {
    edit(item) {
      this.$emit("updateproduct", item);
    },
    alertOnDelete(item) {
      this.$q
        .dialog({
          title: "Atenção!",
          message: `Você deseja realmente deletar ${item.name}?`,
          cancel: true
        })
        .onOk(() => {
          this.deleteProduct(item.id);
        })
        .onCancel(() => {
          this.$q.notify("Nenhum produto deletado");
        });
    },
    deleteProduct(id) {
      // console.log("ID: ", id);
      this.$axios
        .delete(`products/${id}`)
        .then(res => {
          this.$q.notify("Produto deletado");
          this.$emit("refresh");
        })
        .catch(e => {
          this.$q.notify("Não foi possivel deletar este produto");
          console.log(e);
        });
    }
  }
};
</script>
