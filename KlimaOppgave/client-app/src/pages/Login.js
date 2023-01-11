import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import AccountForm from "../components/AccountForm";

const Login = ({ setSessionBrukernavn }) => {
  //State på verdien av brukernavn og passord-inputfeltet
  const [brukernavn, setBrukernavn] = useState("");
  const [passord, setPassord] = useState("");

  //State på feilmeldingene
  const [errorMessage, setErrorMessage] = useState("");
  const [brukernavnErrorMessage, setBrukernavnErrorMessage] = useState("");
  const [passordErrorMessage, setPassordErrorMessage] = useState("");

  //State på teksten i Logg inn knappen
  const [buttonStatus, setButtonStatus] = useState("Logg inn");

  //State til bootstrap-klassene til brukernavn-inputfeltet og passord-inputfeltet
  const [brukernavnInputState, setBrukernavnInputState] =
    useState("border-secondary");
  const [passordInputState, setPassordInputState] =
    useState("border-secondary");

  //Brukes til navigasjon
  const navigate = useNavigate();

  //Når jeg klikke på inputfeltet så endrer kanten til inputfeltet til blå
  const onFocusBrukerInput = () => {
    setBrukernavnInputState("border-primary");
  };

  //Når jeg klikker bort så blir den igjen til grått
  const onBlurBrukerInput = () => {
    setBrukernavnInputState("border-secondary");
  };

  //Når jeg klikke på inputfeltet så endrer kanten til inputfeltet til blå
  const onFocusPassordInput = () => {
    setPassordInputState("border-primary");
  };

  //Når jeg klikker bort så blir den igjen til grått
  const onBlurPassordInput = () => {
    setPassordInputState("border-secondary");
  };

  //Sjekker inputvalidering
  const onChangeBrukernavn = (event) => {
    const inputValue = event.target.value;
    setBrukernavn(inputValue);

    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;

    if (!regexp.test(inputValue)) {
      setBrukernavnErrorMessage("Brukernavnet må bestå av 2 til 20 bokstaver");
    } else {
      setBrukernavnErrorMessage("");
    }
  };

  const onChangePassord = (event) => {
    const inputValue = event.target.value;
    setPassord(event.target.value);

    const regexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;

    if (!regexp.test(inputValue)) {
      setPassordErrorMessage(
        "Passordet må bestå minimum 6 tegn, minst en bokstav og et tall"
      );
    } else {
      setPassordErrorMessage("");
    }
  };

  const login = async (event) => {
    // Lager et objekt som inneholder brukernavn og passord
    // og skal sendes til server
    const user = { brukernavn: brukernavn, passord: passord };

    // Sjekker om brukernavn og input feltet er tomt
    if (brukernavn !== "" && passord !== "") {
      // Sender POST metode til logginn
      axios
        .post("/logginn", user)
        .then((res) => {
          // Hvis res.data er true så er det slikt at man logger inn vellyket
          if (res.data === true) {
            setButtonStatus(
              <div
                className="spinner-border spinner-border-sm text-light"
                role="status"
              >
                <span className="visually-hidden"></span>
              </div>
            );

            //Henter data til sessionen
            // slikt at jeg får sessionBrukernavn
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

            // Venter 1 sekund med å navigere til hovedsiden
            // slik at dataene kan lastes inn
            setTimeout(() => {
              navigate("/");
            }, 1000);
          }
        })
        .catch((error) => {
          setErrorMessage(error.response.data);
        });
    }

    // Hvis brukernavn er tom da oppdateres
    // state til de error-variablene
    if (brukernavn === "") {
      setBrukernavnErrorMessage("Brukernavn må fylles ut");
      setBrukernavnInputState("border-danger");
    }
    if (passord === "") {
      setPassordErrorMessage("Passord må fylles ut");
      setPassordInputState("border-danger");
    }

    event.preventDefault();
  };

  // Sender de nødvendige variablene til
  // AccountForm-komponenten
  return (
    <>
      <AccountForm
        formTitle="Logg inn"
        onSubmit={login}
        brukernavn={brukernavn}
        passord={passord}
        brukernavnErrorMessage={brukernavnErrorMessage}
        passordErrorMessage={passordErrorMessage}
        onChangeBrukernavn={onChangeBrukernavn}
        onChangePassord={onChangePassord}
        path="register"
        text="Har du ikke konto? Klikk her"
        errorMessage={errorMessage}
        buttonStatus={buttonStatus}
        onFocusBrukerInput={onFocusBrukerInput}
        onBlurBrukerInput={onBlurBrukerInput}
        onFocusPassordInput={onFocusPassordInput}
        onBlurPassordInput={onBlurPassordInput}
        brukernavnInputState={brukernavnInputState}
        passordInputState={passordInputState}
      />
    </>
  );
};

export default Login;
