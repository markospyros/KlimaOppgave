import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

const AnswerInput = (props) => {
  const [svarText, setSvarText] = useState("");
  const [buttonState, setButtonState] = useState("btn-dark");
  const [inputState, setInputState] = useState("border-secondary");
  const [errorMessageStatus, setErrorMessageStatus] = useState("none");

  const navigate = useNavigate();

  const onFocus = () => {
    setInputState("border-primary");
    const svarTextWithoutSpace = svarText.trim();

    if (svarTextWithoutSpace.length !== 0) {
      setButtonState("btn-primary");
    }
  };

  const onBlur = () => {
    setInputState("border-secondary");
    setErrorMessageStatus("none");

    const svarTextWithoutSpace = svarText.trim();

    if (svarTextWithoutSpace.length === 0) {
      setButtonState("btn-dark");
    }
  };

  const onChangeText = (event) => {
    setInputState("border-primary");
    setErrorMessageStatus("none");

    const inputValue = event.target.value;
    setSvarText(inputValue);

    if (inputValue.trim().length === 0) {
      setButtonState("btn-dark");
    } else {
      setButtonState("btn-primary");
    }
  };

  const submitForm = (event) => {
    const svarTextWithoutSpace = svarText.trim();
    if (svarTextWithoutSpace.length !== 0) {
      const svar = {
        innhold: svarText,
        innleggId: props.innleggId,
        brukernavn: props.sessionBrukernavn,
      };

      axios
        .post("/leggsvar", svar)
        .then((response) => {
          svar.svarId = response.data.svarId;
          svar.dato = response.data.dato;
          //Henter addAnswer-funksjonen fra hovedsiden,
          // som oppdaterer state til post fra hovedsiden
          props.addAnswer(svar, svar.innleggId);
          setSvarText("");
        })
        .catch((error) => {
          if (error.response.status === 401) {
            navigate("/login");
          }
        });
    } else {
      setInputState("border-danger");
      setErrorMessageStatus("block");
    }

    event.preventDefault();
  };

  return (
    <div className="card-footer">
      <form onSubmit={submitForm} className="input-group">
        <input
          value={svarText}
          onFocus={onFocus}
          onBlur={onBlur}
          onChange={onChangeText}
          type="text"
          className={`form-control form-control-outline ${inputState} border-3 shadow-none`}
          placeholder="Legg inn et svar"
        />

        <button
          className={`btn ${buttonState}`}
          type="button"
          onClick={submitForm}
        >
          Svar
        </button>
      </form>
      <div className={`invalid-feedback d-${errorMessageStatus}`}>
        Svar må fylles ut
      </div>
    </div>
  );
};

export default AnswerInput;
