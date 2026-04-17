import axios from 'axios'

const API_URL = 'http://localhost:5232/api/Alumnos'

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
