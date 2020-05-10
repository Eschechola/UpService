using System;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;
using ESCHENet.Emails.Functions;
using ESCHENet.Emails.Model;

namespace UpServiceAPI.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSender _emailSender;

        public EmailService(EmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void SendAcceptedOffer(Job job, string emailProvider, double valueAccept)
        {
            var email = new Email
            {
                Receiver = emailProvider,
                Subject = "UpService - Seu orçamento foi aceito. Mãos na massa!!!!",
                Body = String.Format(@"
                        <h2 align=""center"">Seu orçamento foi aceito pelo cliente, agora é hora de botar a mão na massa e entregar o melhor possível!!</h2>
                        <br>
                        <br>
                        <h3><strong>Dados do trabalho:</strong></h3>
                        
                        <strong>Título</strong>: {0}
                            
                        <br>
                        <strong>Data de publicação</strong>: {1}
                        
                        <br>
                        <strong>Descrição: </strong> {2}
                        
                        <br>
                        <strong>Com um valor de: </strong>R${3}


                        <br><br>
                        <h4><strong>* O CLIENTE IRÁ FALAR COM VOCÊ ATRAVÉS DAS SUAS INFORMAÇÕES DEIXADAS NO SEU CADASTRO NO APLICATIVO, FIQUE DE OLHO NO SEU CELULAR E EMAIL!</strong></h4>
                        
                        <br><br><br><br><br><br><br>
                        <center><p>Atenciosamente, Equipe UpService. &copy;</p></center>
                        
                ", job.Title, job.PublicationDate, job.Description, valueAccept)
            };

            _emailSender.SendEmail(email, isHtml: true);
        }

        public void SendFinishJob(Job job, string emailProvider)
        {
            var email = new Email
            {
                Receiver = emailProvider,
                Subject = "UpService - Serviço finalizado!!!!",
                Body = String.Format(@"
                        <h2 align=""center"">Seu serviço foi finalizado com êxito!</h2>
                        <br>
                        <br>
                        <h3>{0} - FINALIZADO</h3>
                        <p>
                            O cliente sinalizou que você finalizou o serviço com sucesso e que o pagamento foi efetuado,
                            muito obrigado por usar nosso aplicativo, lembrando que nós não nos responsabilizamos por eventuais problemas no pagamento.
                        </p>
                 ", job.Title)
            };

            _emailSender.SendEmail(email, isHtml: true);
        }

        public void SendOffer(Client requester, Client provider, Job job, double offerValue)
        {
            var email = new Email
            {
                Receiver = requester.Email,
                Subject = "Um prestador de serviços lhe enviou uma proposta - UpService",
                Body = String.Format(@"
                            <h2 align=""center"">Olá, aqui é a equipe de suporte do <strong>Up Service</strong> e estamos aqui pra te avisar que um prestador de serviços está interessado no seu projeto !!</h2>
                            <br>
                            
                            <br>

                            <h3><strong>Informações do projeto no qual ele está interessado:</strong></h3>
                            
                            <strong>Título</strong>: {0}
                            
                            <br>
                            <strong>Data de publicação</strong>: {1}
                            
                            <br>
                            <strong>Código</strong>: {2}
                            
                            <br>
                            <br>
                            <br>
                            <br>
                            
                            <h3><strong>Dados do prestador de serviço:</strong></h3>
                            
                            <strong>Nome</strong>: {3}
                            
                            <br>
                            <strong>CPF</strong>: {4}
                            
                            <br>
                            <strong>Email</strong>: {5}
                            
                            <br>
                            <strong>Telefone</strong>: {6}
                                
                            <br>
                            <strong>Com um valor estimado de</strong>: R${7}
                            
                            <br><br><br><br>
                            
                            <center><a href=" + "https://localhost:44333/api/v1/job/accept-offer?jobHash={8}&providerId={9}&valueOffer={10}" + ">Clique aqui para aceitar a proposta</a></center>" +

                            "<br><br><br><br><br><br><br>" +
                            "<center><p>Atenciosamente, Equipe UpService. &copy;</p></center>", job.Title, job.PublicationDate, job.Hash, provider.Name, provider.CPF, provider.Email, provider.Telephone, offerValue, job.Hash, provider.Id, offerValue)
            };

            _emailSender.SendEmail(email, isHtml: true);
        }
    }
}
