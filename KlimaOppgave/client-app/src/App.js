import axios from "axios";
import React, { useEffect, useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AskQuestion from "./pages/AskQuestion";
import EditQuestion from "./pages/EditQuestion";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import SharedLayout from "./pages/SharedLayout";

const App = () => {
  const [user, setUser] = useState(null);

  return (
    <BrowserRouter>
      <Routes>
        <Route path="login" element={<Login setUser={setUser}></Login>} />
        <Route
          path="register"
          element={<Register setUser={setUser}></Register>}
        />
        <Route path="/" element={<SharedLayout />}>
          <Route index element={<Home user={user} />} />
          <Route path="askquestion" element={<AskQuestion user={user} />} />
          <Route path="edit/:id" element={<EditQuestion user={user} />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
