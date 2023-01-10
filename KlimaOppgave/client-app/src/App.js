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
  const [sessionBrukernavn, setSessionBrukernavn] = useState(null);

  axios
    .get("/getsessiondata")
    .then((res) => {
      setSessionBrukernavn(res.data);
    })
    .catch((error) => {
      if (error.status.response === 401) {
        setSessionBrukernavn("");
      }
    });

  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="login"
          element={<Login setSessionBrukernavn={setSessionBrukernavn}></Login>}
        />
        <Route
          path="register"
          element={
            <Register setSessionBrukernavn={setSessionBrukernavn}></Register>
          }
        />
        <Route
          path="/"
          element={<SharedLayout sessionBrukernavn={sessionBrukernavn} />}
        >
          <Route
            index
            element={<Home sessionBrukernavn={sessionBrukernavn} />}
          />
          <Route
            path="askquestion"
            element={<AskQuestion sessionBrukernavn={sessionBrukernavn} />}
          />
          <Route
            path="edit/:id"
            element={<EditQuestion sessionBrukernavn={sessionBrukernavn} />}
          />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
