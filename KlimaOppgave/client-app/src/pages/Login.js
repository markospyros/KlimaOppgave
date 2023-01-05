import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import AccountForm from "../components/AccountForm";

const Login = ({ setUser }) => {
  const [brukernavn, setBrukernavn] = useState("");
  const [passord, setPassord] = useState("");

  const navigate = useNavigate();

  const onChangeBrukernavn = (event) => {
    setBrukernavn(event.target.value);
  };

  const onChangePassord = (event) => {
    setPassord(event.target.value);
  };

  const login = async (event) => {
    const user = { brukernavn: brukernavn, passord: passord };

    axios
      .post("/logginn", user)
      .then((res) => {
        user.brukerId = res.data;
        setUser(user);
        navigate("/");
      })
      .catch((error) => {
        console.log(error.response.data);
      });

    event.preventDefault();
  };

  return (
    <>
      <AccountForm
        formTitle="Logg inn"
        onSubmit={login}
        brukernavn={brukernavn}
        passord={passord}
        onChangeBrukernavn={onChangeBrukernavn}
        onChangePassord={onChangePassord}
        path="register"
        text="Har du ikke konto? Klikk her"
        buttonText="Logg inn"
      />
    </>
  );
};

export default Login;
