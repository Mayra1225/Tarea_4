import axios from 'axios'

const API_URL = 'https://tarea-4-aonp.onrender.com/api/Alumno'

const api = axios.create({
  baseURL: API_URL,
})

export default {
  getAlumnos() {
    return api.get('/')
  },
  getAlumno(id) {
    return api.get(`/${id}`)
  },
  crearAlumno(data) {
    return api.post('/', data)
  },
  editarAlumno(id, data) {
    return api.put(`/${id}`, data)
  },
  eliminarAlumno(id) {
    return api.delete(`/${id}`)
  },
}
