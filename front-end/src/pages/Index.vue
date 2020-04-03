<template>
  <q-page padding>
    <q-btn color="primary" label="Adicionar Produto" class="q-my-md" @click="modal = true" />

    <p-list
      :products="products"
      @updateproduct="value => editProduct(value)"
      @refresh="getProducts"
    />

    <q-dialog v-model="modal" @hide="clearModal">
      <q-card>
        <q-bar>
          <q-space />
          <q-btn dense flat icon="close" v-close-popup>
            <q-tooltip content-class="bg-white text-primary">Fechar</q-tooltip>
          </q-btn>
        </q-bar>
        <q-card-section style="min-width: 50vh; min-height: 40vh">
          <div class="text-h5 q-mb-md">Produto {{ product.id ? product.name : 'novo' }}</div>
          <div class="row q-mb-sm">
            <div class="col">
              <q-input outlined v-model="product.name" label="Nome" />
            </div>
          </div>

          <div class="row q-gutter-sm q-mb-sm">
            <div class="col">
              <q-input
                outlined
                type="number"
                v-model.number="product.quantityInPackage"
                label="Quantidade"
              />
            </div>

            <div class="col">
              <q-select
                outlined
                v-model="product.unitOfMeasurement"
                :options="optionsMeasurement"
                label="Un. Medida"
                emit-value
                map-options
              />
            </div>
          </div>

          <q-input outlined type="number" v-model.number="product.price" label="Preço" />
        </q-card-section>
        <q-card-actions class="row justify-end">
          <q-btn v-if="product.id" class="bg-info text-white on-left" label="Editar" v-close-popup />
          <q-btn v-else class="bg-positive text-white on-left" label="Salvar" @click="saveProduct" />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
import pList from "../components/listProducts";

export default {
  name: "PageIndex",
  components: {
    pList
  },
  data() {
    return {
      modal: false,
      product: {
        name: "",
        quantityInPackage: "",
        unitOfMeasurement: "",
        categoryId: 102,
        price: ""
      },
      products: [],
      optionsMeasurement: [
        { label: "Unidade - un", value: 1 },
        { label: "Miligrama - mg", value: 2 },
        { label: "Grama - gr", value: 3 },
        { label: "Kilo - kg", value: 4 },
        { label: "Litro - lt", value: 5 }
      ]
    };
  },
  methods: {
    clearModal() {
      this.product = {
        name: "",
        quantityInPackage: "",
        unitOfMeasurement: "",
        categoryId: 102,
        price: ""
      };
      this.modal = false;
    },
    getProducts() {
      this.$q.loading.show();
      this.$axios
        .get("products/")
        .then(res => {
          this.$q.loading.hide();
          this.products = res.data.items;
        })
        .catch(e => {
          this.$q.loading.hide();
          console.log(e);
        });
    },
    editProduct(item) {
      this.modal = true;
      this.product = item;
    },
    updateEdition() {
      this.$q.loading.show();
      this.$axios
        .put(`products/${product.id}`, this.product)
        .then(res => {
          this.$q.loading.hide();
          this.getProducts();
          this.modal = false;
        })
        .catch(e => {
          this.$q.loading.hide();
          console.log(e);
        });
    },
    saveProduct() {
      this.$q.loading.show();
      this.$axios
        .post("products/", this.product)
        .then(res => {
          this.$q.loading.hide();
          this.getProducts();
          this.modal = false;
        })
        .catch(e => {
          this.$q.loading.hide();
          console.log(e);
        });
    }
  },
  mounted() {
    this.getProducts();
  }
};
</script>

