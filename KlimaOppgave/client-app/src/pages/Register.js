import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import AccountForm from "../components/AccountForm";

const Register = ({ setSessionBrukernavn }) => {
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

  //Brukes til navigasjon
  const navigate = useNavigate();

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

  // Lager bruker
  const createAccount = async (event) => {
    // Lager et objekt som inneholder brukernavn og passord
    // og skal sendes til server
    const user = { brukernavn: brukernavn, passord: passord };

    // Sjekker om brukernavn og input feltet er tomt
    if (brukernavn !== "" && passord !== "") {
      // Sender POST metode til lagbruker til server
      axios
        .post("/lagbruker", user)
        .then(() => {
          setButtonStatus(
            <div
              className="spinner-border spinner-border-sm text-light"
              role="status"
            >
              <span className="visually-hidden"></span>
            </div>
          );

          // Henter dataene til den sessionen
          axios
            .get("/getsessiondata")
            .then((res) => {
              setSessionBrukernavn(res.data);
            })
            .catch((error) => {
              //Hvis ikke logget inn
              if (error.status.response === 401) {
                setSessionBrukernavn("");
                //Navigerer til login
                navigate("/login");
              }
            });

          setTimeout(() => {
            navigate("/");
          }, 1000);
        })
        // Henter feilmeldingene for lagbruker
        .catch((error) => {
          setErrorMessage(error.response.data);
        });
    }

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

  return (
    <>
      <AccountForm
        formTitle="Registrer"
        onSubmit={createAccount}
        brukernavn={brukernavn}
        passord={passord}
        brukernavnErrorMessage={brukernavnErrorMessage}
        passordErrorMessage={passordErrorMessage}
        onChangeBrukernavn={onChangeBrukernavn}
        onChangePassord={onChangePassord}
        path="login"
        text="Har du allerede konto? Klikk her"
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

export default Register;
