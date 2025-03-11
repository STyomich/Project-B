import { Outlet } from "react-router-dom"
import Header from "./layouts/Header"

function App() {

  return (
    <>
      <Header />
      <Outlet />
    </>
  )
}

export default App
