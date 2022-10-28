using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public TestRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Test>> GetListActiveTest()
        {
            var testMember = await _context.Tests
                .Where(u => u.Status == Status.ACTIVE)
                .Include(i => i.Creator)
                .Include(u => u.QuizType)
                .ToListAsync();

            return testMember;
        }

        public async Task<IEnumerable<Test>> GetListActiveTestGuest()
        {
            var testMember = await _context.Tests
                  .Where(u => u.Status == Status.ACTIVE)
                  .ToListAsync();
            return testMember;
        }

        public async Task<Test> GetTestById(int testId)
        {
            var testDetail = await _context.Tests
                .Where(a => a.Id == testId)
                .Include(i => i.Creator)
                .Include(u => u.QuizType)
                .FirstOrDefaultAsync();
            return testDetail;
        }

        public async Task<Test> GetTestToAttemp(int testId)
        {
            var testMember = await _context.Tests
                .Where(a => a.Id == testId)
                .Include(u => u.Questions.Where(a => a.Status == Status.ACTIVE))
                .ThenInclude(q => q.Options.Where(a => a.Status == Status.ACTIVE))
                .Where(a => a.Status == Status.ACTIVE)
                .FirstOrDefaultAsync();
            return testMember;
        }

       

        public async Task<Test> CreateTest(CreateTestRequest request, Guid userId)
        {
            try
            {
                Test test = new()
                {
                    CreatorId = userId,
                    QuizName = request.QuizName,
                    QuizTypeId = request.QuizTypeId,
                    TotalQuestion = request.TotalQuestion,
                    CreatedDate = DateTime.Now,
                    Introduction =  request.Introduction,
                    History = request.History,
                    CriteriaInformation = request.CriteriaInformation,
                    BannerUrl = request.BannerUrl,
                    BackgroundUrl = request.BackgroundUrl, 
                    Status = Status.PENDING
                };
                await _context.Tests.AddAsync(test);
                await _context.SaveChangesAsync();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Models.TestType>> GetActiveTestType()
        {
            var listTestType = await _context.TestTypes
                .Where(u => u.Status == Status.ACTIVE)
                .ToListAsync();

            return listTestType;
        }

        public async Task<IEnumerable<Test>> GetListAllTest(Guid userId)
        {
            var testMember = await _context.Tests
                .Include(u => u.QuizType)
                .Include(i => i.Creator)
                .ToListAsync();

            return testMember;
        }

        public async Task<bool> CreateResult(ResultRequest request)
        {
            try
            {
                Character addResult = new()
                {
                   Id =Guid.NewGuid(),
                   TestId = request.TestId,
                   ResultName = request.ResultName,
                   ResultRelationship = request.ResultRelationship,
                   ResultSuccessRule   = request.ResultSuccessRule,
                   ResultSummary = request.ResultSummary,
                   ResultShortName = request.ResultShortName,
                   ResultPicture = request.ResultPicture,
                   ResultCareer = request.ResultCareer
                };
                await _context.Characters.AddAsync(addResult);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Test> UpdateTestInformation(UpdateTestRequest test)
        {
            try
            {
                var testDb = await _context.Tests
                    .Where(u => u.Id == test.Id)
                    .FirstOrDefaultAsync();
                if (testDb != null)
                {
                    testDb.QuizName = test.QuizName;
                    testDb.TotalQuestion = test.TotalQuestion;
                    testDb.Introduction = test.Introduction;
                    testDb.History = test.History;
                    testDb.CriteriaInformation = test.CriteriaInformation;
                    testDb.BannerUrl = test.BannerUrl;
                    testDb.BackgroundUrl = test.BackgroundUrl;
                } 
                else
                {
                    { throw new Exception("Test does not exist"); }
                }

                await _context.SaveChangesAsync();
                return testDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SubmitResponse> CalculateTestResult(CalculateResultRequest request)
        {
            try
            {
                string typeResult = "";
                var type = await _context.Tests
                    .Where(i => i.Id == request.TestId)
                    .Include(u => u.QuizType)
                    .FirstOrDefaultAsync();

                if (type.QuizType.QuizTypeName.Equals(TestTypeName.MBTI))
                {
                    
                    int
                        isI = 0,
                        isE = 0,
                        isS = 0,
                        isN = 0,
                        isT = 0,
                        isF = 0,
                        isJ = 0,
                        isP = 0;
                    foreach (var question in request.questions)
                    {
                            switch (question.Options.FirstOrDefault().optionValue)
                            {
                                case TypeMBTI.IsI:
                                    isI += 1;
                                    break;

                                case TypeMBTI.IsE:
                                    isE += 1;
                                    break;

                                case TypeMBTI.IsS:
                                    isS += 1;
                                    break;

                                case TypeMBTI.IsN:
                                    isN += 1;
                                    break;

                                case TypeMBTI.IsT:
                                    isT += 1;
                                    break;

                                case TypeMBTI.IsF:
                                    isF += 1;
                                    break;

                                case TypeMBTI.IsJ:
                                    isJ += 1;
                                    break;

                                case TypeMBTI.IsP:
                                    isP += 1;
                                    break;

                                default:
                                    { throw new Exception("Answer wrong"); }
                            }
                    }
                    if (isI > isE)
                    {
                        typeResult = "I";
                    } 
                    else
                    {
                        typeResult = "E";
                    }
                    if (isN > isS)
                    {
                        typeResult += "N";
                    }
                    else
                    {
                        typeResult += "S";
                    }
                    if (isF > isT)
                    {
                        typeResult += "F";
                    }
                    else
                    {
                        typeResult += "T";
                    }
                    if (isJ > isP)
                    {
                        typeResult += "J";
                    }
                    else
                    {
                        typeResult += "P";
                    }
                    //if (isI > isE)
                    //{
                    //    typeResult.Insert(0,"I");
                    //}
                    //else if (isE >= isI)
                    //{
                    //    typeResult.Insert(0, "E");
                    //}

                    //if (isN > isS)
                    //{
                    //    typeResult.Insert(1, "N");
                    //}
                    //else if (isS >= isN)
                    //{
                    //    typeResult.Insert(1, "S");
                    //}

                    //if (isF > isT)
                    //{
                    //    typeResult.Insert(2, "F");
                    //}
                    //else if (isT >= isF)
                    //{
                    //    typeResult.Insert(2, "T");
                    //}

                    //if (isJ > isP)
                    //{
                    //    typeResult.Insert(3, "J");
                    //}
                    //else if (isP >= isJ)
                    //{
                    //    typeResult.Insert(3, "P");
                    //}


                }
                var result = await _context.Characters
                    .Where(a => a.ResultShortName == typeResult)
                    .FirstOrDefaultAsync();
                SubmitResponse response = new()
                {
                    Id = request.TestId,
                    ResultShortName = result.ResultShortName
                }; 
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<string> CalculateTestResultTest(CalculateResultRequest request)
        {
            try
            {
                string typeResult = "";
                var type = await _context.Tests
                    .Where(i => i.Id == request.TestId)
                    .Include(u => u.QuizType)
                    .FirstOrDefaultAsync();

                if (type.QuizType.QuizTypeName.Equals(TestTypeName.MBTI))
                {

                    int
                        isI = 0,
                        isE = 0,
                        isS = 0,
                        isN = 0,
                        isT = 0,
                        isF = 0,
                        isJ = 0,
                        isP = 0;
                    foreach (var question in request.questions)
                    {
                        switch (question.Options.FirstOrDefault().optionValue)
                        {
                            case TypeMBTI.IsI:
                                isI += 1;
                                break;

                            case TypeMBTI.IsE:
                                isE += 1;
                                break;

                            case TypeMBTI.IsS:
                                isS += 1;
                                break;

                            case TypeMBTI.IsN:
                                isN += 1;
                                break;

                            case TypeMBTI.IsT:
                                isT += 1;
                                break;

                            case TypeMBTI.IsF:
                                isF += 1;
                                break;

                            case TypeMBTI.IsJ:
                                isJ += 1;
                                break;

                            case TypeMBTI.IsP:
                                isP += 1;
                                break;

                            default:
                                { throw new Exception("Answer wrong"); }
                        }
                    }
                    if (isI > isE)
                    {
                        typeResult = "I";
                    }
                    else
                    {
                        typeResult = "E";
                    }
                    if (isN > isS)
                    {
                        typeResult += "N";
                    }
                    else
                    {
                        typeResult += "S";
                    }
                    if (isF > isT)
                    {
                        typeResult += "F";
                    }
                    else
                    {
                        typeResult += "T";
                    }
                    if (isJ > isP)
                    {
                        typeResult += "J";
                    }
                    else
                    {
                        typeResult += "P";
                    }
                   

                }
                var result = await _context.Characters
                    .Where(a => a.ResultShortName == typeResult)
                    .FirstOrDefaultAsync();

                return typeResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Character> GetCharacterResult(int testId, string? resultShortName)
        {
            try
            {
                var result = await _context.Characters
                                    .Where(a => a.TestId == testId && a.ResultShortName.Equals(resultShortName))
                                    .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<IEnumerable<Character>> GetAllCharacterResult(int testId)
        {
            try
            {
                var result = await _context.Characters
                                    .Where(a => a.TestId == testId)
                                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
