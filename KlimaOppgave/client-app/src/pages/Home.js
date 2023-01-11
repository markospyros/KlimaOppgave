import axios from "axios";
import React, { useEffect, useState } from "react";
import AnswerInput from "../components/AnswerInput";
import AnswerComponent from "../components/AnswerComponent";
import PostComponent from "../components/PostComponent";
import { useNavigate } from "react-router-dom";
import AskQuestionButton from "../components/AskQuestionButton";

const Home = ({ sessionBrukernavn }) => {
  // Lager et tomt array for innlegg
  const [posts, setPosts] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    // Kaller inn hentinnlegg fra server,
    // henter dataene og oppdaterer posts
    axios
      .get("/hentinnlegg")
      .then((response) => setPosts(response.data))
      .catch((error) => {
        // Hvis ikke bruker er logget inn
        // da hentes ikke noe data
        // og navigerer til login page
        if (error.response.status === 401) {
          navigate("/login");
        }
      });
  }, []);

  // Dataene våre har et attribute som heter timestamp
  // Via den så kan vi sortere posts basert på tiden
  const sortBasedOnTime = () => {
    for (let i = 0; i < posts.length - 1; i++) {
      if (posts[i].timeStamp < posts[i + 1].timeStamp) {
        const tmp = posts[i];
        posts[i] = posts[i + 1];
        posts[i + 1] = tmp;

        i = -1;
      }
    }

    return posts;
  };

  // Kaller funksjonen
  sortBasedOnTime();

  // legger til ny svar til post array
  const addAnswer = (newAnswer, postId) => {
    const updatedPosts = posts.map((post) => {
      if (post.innleggId === postId) {
        post.svar.push(newAnswer);
      }
      return post;
    });
    // Oppdaterer state til posts
    setPosts(updatedPosts);
  };

  // Oppdaterer svar
  const updateAnswer = (updatedAnswer) => {
    const updatedPosts = posts.map((post) => {
      if (post.innleggId === updatedAnswer.innleggId) {
        post.svar = post.svar.map((svar) => {
          if (svar.svarId === updatedAnswer.svarId) {
            return updatedAnswer;
          }
          return svar;
        });
      }
      return post;
    });

    // Oppdaterer state til posts
    setPosts(updatedPosts);
  };

  // Sletter svar
  const deleteAnswer = (id) => {
    const updatedPosts = posts.map((post) => {
      post.svar = post.svar.filter((svar) => svar.svarId !== id);
      return post;
    });
    setPosts(updatedPosts);
  };

  // Sletter innlegg
  const deletePost = (id) => {
    const updatedPosts = posts.filter((post) => post.innleggId !== id);

    setPosts(updatedPosts);
  };

  // Her formater funksjonen de datene
  // fra server til de komponentene
  // og gir dem variabler
  const formatPosts = posts.map((post) => {
    return (
      <div
        key={post.innleggId}
        className="card text-bg-light mb-3"
        style={{ maxWidth: "30rem" }}
      >
        <PostComponent
          id={post.innleggId}
          tittel={post.tittel}
          innhold={post.innhold}
          sessionBrukernavn={sessionBrukernavn}
          brukernavn={post.brukernavn}
          dato={post.dato}
          deletePost={deletePost}
        />
        <div>
          {post.svar.map((svar) => (
            <div key={svar.svarId}>
              <AnswerComponent
                svarId={svar.svarId}
                svarInnhold={svar.innhold}
                innleggId={svar.innleggId}
                sessionBrukernavn={sessionBrukernavn}
                brukernavn={svar.brukernavn}
                svarDato={svar.dato}
                updateAnswer={updateAnswer}
                deleteAnswer={deleteAnswer}
              />
            </div>
          ))}
        </div>
        <div>
          <AnswerInput
            sessionBrukernavn={sessionBrukernavn}
            innleggId={post.innleggId}
            addAnswer={addAnswer}
          />
        </div>
      </div>
    );
  });

  return (
    <div>
      <AskQuestionButton />
      <div>{formatPosts}</div>
    </div>
  );
};

export default Home;
