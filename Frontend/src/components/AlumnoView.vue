<template>
  <div class="container">
    <h1>Sistema de Notas</h1>

    <!-- TABS -->
    <div class="tabs">
      <button :class="{ active: tab === 'alumnos' }" @click="tab = 'alumnos'">Alumnos</button>
      <button :class="{ active: tab === 'notas' }" @click="tab = 'notas'">Notas</button>
      <button :class="{ active: tab === 'tabla' }" @click="tab = 'tabla'">Tabla</button>
    </div>

    <!-- TAB 1 -->
    <div v-if="tab === 'alumnos'" class="card">
      <div class="header">
        <h2>Alumnos</h2>
        <button class="primary" @click="abrirAgregar">+ Agregar</button>
      </div>

      <ul class="list">
        <li v-for="a in alumnos" :key="a.id">
          <span>{{ a.codigo }} - {{ a.nombre }} {{ a.apellido }}</span>
        </li>
      </ul>
    </div>

    <!-- TAB 2 -->
    <div v-if="tab === 'notas'" class="card">
      <h2>Consultar Notas</h2>

      <select v-model="selectedId" @change="cargarAlumno">
        <option disabled value="">Seleccione alumno</option>
        <option v-for="a in alumnos" :key="a.id" :value="a.id">
          {{ a.codigo }} - {{ a.nombre }}
        </option>
      </select>

      <div v-if="alumno" class="resultado">
        <p>
          <b>{{ alumno.nombre }} {{ alumno.apellido }}</b>
        </p>
        <p>P1: {{ alumno.p1 }}</p>
        <p>P2: {{ alumno.p2 }}</p>
        <p class="highlight">P3 necesario: {{ necesario.toFixed(2) }}</p>
      </div>
    </div>

    <!-- TAB 3 -->
    <div v-if="tab === 'tabla'" class="card">
      <h2>Tabla General</h2>

      <table>
        <thead>
          <tr>
            <th>Código</th>
            <th>Nombre</th>
            <th>P1</th>
            <th>P2</th>
            <th>P3</th>
            <th>Final</th>
            <th>Acciones</th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="a in alumnos" :key="a.id">
            <td>{{ a.codigo }}</td>
            <td>{{ a.nombre }} {{ a.apellido }}</td>
            <td>{{ a.p1 }}</td>
            <td>{{ a.p2 }}</td>
            <td>{{ calcularNecesario(a).toFixed(2) }}</td>
            <td>{{ calcularFinal(a).toFixed(2) }}</td>

            <td>
              <button class="edit" @click="abrirEditar(a)">Editar</button>
              <button class="delete" @click="eliminarAlumno(a.id)">Eliminar</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- MODAL AGREGAR -->
    <div v-if="showAdd" class="modal">
      <div class="modal-content">
        <h3>Agregar Alumno</h3>

        <input v-model="nuevo.codigo" placeholder="Código" />
        <input v-model="nuevo.nombre" placeholder="Nombre" />
        <input v-model="nuevo.apellido" placeholder="Apellido" />
        <input type="number" v-model.number="nuevo.p1" placeholder="P1" />
        <input type="number" v-model.number="nuevo.p2" placeholder="P2" />

        <button class="primary" @click="agregarAlumno">Guardar</button>
        <button @click="showAdd = false">Cancelar</button>
      </div>
    </div>

    <!-- MODAL EDITAR -->
    <div v-if="showEdit" class="modal">
      <div class="modal-content">
        <h3>Editar Alumno</h3>

        <input v-model="edit.codigo" disabled />
        <input v-model="edit.nombre" />
        <input v-model="edit.apellido" />
        <input type="number" v-model.number="edit.p1" />
        <input type="number" v-model.number="edit.p2" />

        <button class="primary" @click="guardarEdicion">Guardar</button>
        <button @click="showEdit = false">Cancelar</button>
      </div>
    </div>
  </div>
</template>

<script>
import api from '../services/api'

export default {
  data() {
    return {
      tab: 'alumnos',
      alumnos: [],
      selectedId: '',
      alumno: null,
      necesario: 0,

      showAdd: false,
      showEdit: false,

      nuevo: { codigo: '', nombre: '', apellido: '', p1: 0, p2: 0 },
      edit: {},
    }
  },

  methods: {
    abrirAgregar() {
      this.showAdd = true
    },

    abrirEditar(a) {
      this.edit = { ...a }
      this.showEdit = true
    },

    async cargarAlumnos() {
      const res = await api.getAlumnos()
      this.alumnos = res.data
    },

    async cargarAlumno() {
      const res = await api.getAlumno(this.selectedId)
      this.alumno = res.data.alumno
      this.necesario = res.data.necesario
    },

    async agregarAlumno() {
      await api.crearAlumno(this.nuevo)
      this.showAdd = false
      this.nuevo = { codigo: '', nombre: '', apellido: '', p1: 0, p2: 0 }
      this.cargarAlumnos()
    },

    async guardarEdicion() {
      await api.editarAlumno(this.edit.id, {
        nombre: this.edit.nombre,
        apellido: this.edit.apellido,
        p1: this.edit.p1,
        p2: this.edit.p2,
      })

      this.showEdit = false
      this.cargarAlumnos()
    },

    async eliminarAlumno(id) {
      if (!confirm('¿Eliminar?')) return
      await api.eliminarAlumno(id)
      this.cargarAlumnos()
    },

    calcularNecesario(a) {
      return (6 - (a.p1 * 0.25 + a.p2 * 0.35)) / 0.4
    },

    calcularFinal(a) {
      return a.p1 * 0.25 + a.p2 * 0.35 + this.calcularNecesario(a) * 0.4
    },
  },

  mounted() {
    this.cargarAlumnos()
  },
}
</script>

<style>
body {
  margin: 0;
  background: linear-gradient(135deg, #1e3c72, #2a5298);
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

/* CONTENEDOR */
.container {
  max-width: 1000px;
  margin: 40px auto;
  padding: 20px;
}

/* TITULO */
h1 {
  text-align: center;
  color: white;
  margin-bottom: 20px;
}

/* TABS */
.tabs {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}

.tabs button {
  flex: 1;
  padding: 12px;
  border-radius: 10px;
  border: none;
  cursor: pointer;
  background: rgba(255, 255, 255, 0.2);
  color: white;
  font-weight: bold;
  transition: 0.3s;
}

.tabs button:hover {
  background: rgba(255, 255, 255, 0.3);
}

.tabs .active {
  background: white;
  color: #2a5298;
}

/* CARD */
.card {
  background: white;
  padding: 25px;
  border-radius: 15px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
}

/* HEADER */
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

/* LISTA */
.list li {
  padding: 12px;
  border-radius: 8px;
  margin-bottom: 5px;
  background: #f7f9fc;
  transition: 0.2s;
}

.list li:hover {
  background: #eaf1ff;
}

/* INPUTS */
input,
select {
  width: 100%;
  padding: 10px;
  margin-top: 10px;
  border-radius: 8px;
  border: 1px solid #ccc;
  outline: none;
  transition: 0.2s;
}

input:focus,
select:focus {
  border-color: #3498db;
  box-shadow: 0 0 5px rgba(52, 152, 219, 0.5);
}

/* BOTONES */
button {
  margin-top: 10px;
  padding: 10px;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  transition: 0.3s;
}

.primary {
  background: linear-gradient(45deg, #2ecc71, #27ae60);
  color: white;
}

.primary:hover {
  transform: scale(1.05);
}

.edit {
  background: linear-gradient(45deg, #f39c12, #e67e22);
  color: white;
}

.delete {
  background: linear-gradient(45deg, #e74c3c, #c0392b);
  color: white;
}

/* TABLA */
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 15px;
  overflow: hidden;
  border-radius: 10px;
}

th {
  background: #2a5298;
  color: white;
  padding: 12px;
}

td {
  padding: 10px;
  text-align: center;
  background: #f9f9f9;
}

tr:nth-child(even) td {
  background: #eef3ff;
}

/* MODAL */
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
  backdrop-filter: blur(5px);
}

.modal-content {
  background: white;
  padding: 25px;
  border-radius: 15px;
  width: 320px;
  animation: fadeIn 0.3s ease;
}

@keyframes fadeIn {
  from {
    transform: translateY(-20px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}

/* RESULTADO */
.highlight {
  color: #e74c3c;
  font-weight: bold;
  font-size: 18px;
}
</style>
